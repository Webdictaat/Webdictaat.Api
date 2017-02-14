using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using MVCWithAuth.Services;
using Webdictaat.Domain;
using Webdictaat.CMS.Models.AccountViewModels;
using System.Security.Principal;
using Webdictaat.Core.JWT;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Webdictaat.Domain.User;


namespace MVCWithAuth.Controllers
{
    /// <summary>
    /// Controller with methods to authenticate, get current user and logoff.
    /// </summary>
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="emailSender"></param>
        /// <param name="smsSender"></param>
        public AccountController(
              ILoggerFactory loggerFactory,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
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
                return null;
            }
 
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null)
            {
                //Something is wrong with the callback provider
                //Bad cocokies maybe?
                return null;
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);


            if (result.Succeeded)
            {
                _logger.LogInformation(5, "User logged in with {Name} provider.", info.LoginProvider);
                return Redirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return null;
            }
            else
            {
                //Get the information about the user from the external login provider
                var user = new ApplicationUser { UserName = info.Principal.FindFirstValue(ClaimTypes.Email), Email = info.Principal.FindFirstValue(ClaimTypes.Email) };

                var createUserResult = await _userManager.CreateAsync(user);
                if (createUserResult.Succeeded)
                {
                    createUserResult = await _userManager.AddLoginAsync(user, info);
                    if (createUserResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation(6, "User created an account using {Name} provider.", info.LoginProvider);              
                    }
                }
                AddErrors(createUserResult);

                return Redirect(returnUrl);
            }
        }

        /// <summary>
        /// Get the current user if logged in
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("Current")]
        public async Task<Webdictaat.CMS.ViewModels.User> Current()
        {
            ApplicationUser user = await this.GetCurrentUserAsync();
            return new Webdictaat.CMS.ViewModels.User(user);
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
            return _userManager.FindByNameAsync(User.Identity.Name);
        }

        #endregion
    }
}
