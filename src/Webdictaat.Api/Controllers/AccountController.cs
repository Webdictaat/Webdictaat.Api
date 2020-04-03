using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Webdictaat.Domain.User;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Webdictaat.Data;
using Webdictaat.Core;
using Microsoft.Extensions.Options;
using OAuth;
using System.Net;
using System.IO;
using System.Web;
using Webdictaat.Api.Auth;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace MVCWithAuth.Controllers
{
    /// <summary>
    /// Controller with methods to authenticate, get current user and logoff.
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly string _jwtsecret;
        private readonly JsonSerializerSettings _serializerSettings;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly WebdictaatContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private AvansOauthHelperOptions AvansOauthHelperOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="userManager"></param>
        public AccountController(
            IOptions<ConfigVariables> appSettings,
            WebdictaatContext context,
            ILoggerFactory loggerFactory,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();

            this._jwtsecret = appSettings.Value.JWTSECRET;

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
                 
            this.AvansOauthHelperOptions = new AvansOauthHelperOptions()
            {
                AvansClientId = appSettings.Value.AvansOauthClientId, 
                AvansSecret = appSettings.Value.AvansOauthSecret
            };
        }

        /// <summary>
        /// Navigate to this url to login.
        /// Will return 400 if no returnurl is present
        /// </summary>
        /// <param name="returnUrl">Required parameter </param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ExternalLogin")]
        public IActionResult ExternalLogin(string returnUrl)
        {
            if(returnUrl == null)
            {
                return StatusCode(400);
            }

            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return Challenge(properties, "Google");
        }

        /// <summary>
        /// Callback url for the external identity provider
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="remoteError"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return Redirect(returnUrl);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                return Redirect(returnUrl);
            }

            // Sign in the user with this external login provider if the user already has a login.
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var fullName = info.Principal.FindFirstValue(ClaimTypes.Name);

            ApplicationUser user = await this.GetOrCreateUser(email, email, fullName, info);

            var token = GenerateToken(user);
            var url = returnUrl + "#/process-token?token=" + token;
            return Redirect(url);
        }

   
           

        [HttpGet]
        [AllowAnonymous]
        [Route("AvansLogin")]
        public IActionResult AvansLogin(string returnUrl)
        {
            if(String.IsNullOrEmpty(returnUrl))
            {
                return StatusCode(400);
            }

            //get request token
            var oauthToken = AvansOauthHelper.GetRequestToken(this.AvansOauthHelperOptions);


            //store the oauth token, secret and return url temporarily
            HttpContext.Session.SetString("oauth_token", oauthToken.Token);
            HttpContext.Session.SetString("oauth_secret", oauthToken.Secret);
            HttpContext.Session.SetString("returnUrl", returnUrl);

            //redirect to saml screen
            return Redirect("https://publicapi.avans.nl/oauth/saml.php?oauth_token=" + oauthToken.Token);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("AvansCallback")]
        public async Task<IActionResult> AvansCallback()
        {
            //get the token from the session
            var token = new OauthToken()
            {
                Token = HttpContext.Session.GetString("oauth_token"),
                Secret = HttpContext.Session.GetString("oauth_secret")
            };

            var returnUrl = HttpContext.Session.GetString("returnUrl");
            var verifier = HttpContext.Request.Query["oauth_verifier"];

            OauthToken accesToken = AvansOauthHelper.GetAccesToken(this.AvansOauthHelperOptions, token, verifier);

            //clear session of temp data
            HttpContext.Session.Remove("oauth_token");
            HttpContext.Session.Remove("oauth_secret");
            HttpContext.Session.Remove("returnUrl");

            string userInfo = AvansOauthHelper.GetUserInfo(this.AvansOauthHelperOptions, accesToken);
            var avansDetails = JObject.Parse(userInfo);

            var name = (string)avansDetails["nickname"];
            var isEmployee = (string)avansDetails["employee"] == "true";
            var email = (string)avansDetails["emails"][0];
            var username = (string)avansDetails["accounts"]["username"];

            var info = new UserLoginInfo("Avans", "Avans", "Avans");

            ApplicationUser user = await this.GetOrCreateUser(email, username, name, info);

            var jwt = GenerateToken(user);
            returnUrl = returnUrl + "#/process-token?token=" + jwt;


            return Redirect(returnUrl);
        }

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(180)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsecret)),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async void UpdateUserName(ExternalLoginInfo info)
        {
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var user = _userManager.FindByEmailAsync(email).Result;

            if(user.FullName == null)
            {
                user.FullName = info.Principal.FindFirstValue(ClaimTypes.Name);
                user.UserName = email;
                user.NormalizedUserName = user.UserName.ToUpper();
            }

            var updateUserResult = _userManager.UpdateAsync(user).Result;
        }

        /// <summary>
        /// Get the current user if logged in
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("Current")]
        public async Task<Webdictaat.Api.ViewModels.UserVM> Current()
        {
            ApplicationUser user = await this.GetCurrentUserAsync();
            return new Webdictaat.Api.ViewModels.UserVM(user, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("LogOff")]
        public async Task<ActionResult> LogOff(string returnUrl)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return Redirect(returnUrl);
        }

        #region Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _userManager.FindByIdAsync(userId);
        }

        private async Task<ApplicationUser> GetOrCreateUser(string email, string username, string fullname, UserLoginInfo info)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            //Not yet a user
            if (user == null)
            {

                //Get the information about the user from the external login provider
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    FullName = fullname
                };

                var createUserResult = await _userManager.CreateAsync(user);

                if (createUserResult.Succeeded)
                {
                    createUserResult = await _userManager.AddLoginAsync(user, info);
                }
                AddErrors(createUserResult);
            }

            return user;
        }

        #endregion
    }
}
