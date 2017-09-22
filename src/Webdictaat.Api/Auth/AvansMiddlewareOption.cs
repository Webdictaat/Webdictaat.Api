//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Authentication.OAuth;
//using Microsoft.AspNetCore.Builder;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Converters;
//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Linq;
//using System.Net.Http.Headers;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using System.Net.Http;


/***
 * 
 * This code snippet works for OAUTH 2.0 but is not yet supported by Avans
 * 
 * */

//namespace Webdictaat.Api.Auth
//{
//    public class AvansMiddlewareOptions
//    {
//        public static OAuthOptions GetOptions()
//        {
//            var options = new OAuthOptions
//            {
//                AuthenticationScheme = "Avans",
//                ClientId = "cbe30b63a37cb91a3d1565988080e7dcd1e77816",
//                ClientSecret = "366b2a0ef6b52055dadc4e1e07a1b6e906d16cfd",
//                CallbackPath = new PathString("/api/auth/avans/callback"),

//                AuthorizationEndpoint = "https://publicapi.avans.nl/oauth/saml.php",
//                TokenEndpoint = "https://publicapi.avans.nl/oauth/access_token",
//                UserInformationEndpoint = "https://publicapi.avans.nl/oauth/api/user",

//                SaveTokens = true,


//                Events = new OAuthEvents
//                {
//                    OnCreatingTicket = async context => { await CreateAuthTicket(context); }
//                }
//            };

//            return options;
//        }

//        private static async Task CreateAuthTicket(OAuthCreatingTicketContext context)
//        {
//            // Get the User info using the bearer token
//            var request = new System.Net.Http.HttpRequestMessage()
//            {
//                RequestUri = new Uri(context.Options.UserInformationEndpoint),
//                Method = HttpMethod.Get
//            };

//            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
//            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//            var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
//            response.EnsureSuccessStatusCode();

//            var converter = new ExpandoObjectConverter();
//            dynamic user = JsonConvert.DeserializeObject<ExpandoObject>(await response.Content.ReadAsStringAsync(), converter);

//            // Our respone should contain claims we're interested in.
//            context.Identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.name));

//            foreach (var role in user.roles)
//            {
//                context.Identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
//            }
//        }
//    }
//}
