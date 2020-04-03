using OAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Webdictaat.Api.Auth
{
    public class AvansOauthHelper
    {
        public static OauthToken GetRequestToken(string baseUrl, AvansOauthHelperOptions options)
        {
            // Creating a new instance with a helper method
            OAuthRequest client = OAuthRequest.ForRequestToken(options.AvansClientId, options.AvansSecret);
            client.RequestUrl = "https://publicapi.avans.nl/oauth/request_token";
            client.CallbackUrl = baseUrl + "/api/account/AvansCallback";

            // Using URL query authorization to get the request token
            string auth = client.GetAuthorizationQuery();
            var url = client.RequestUrl + "?" + auth;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            Stream receiveStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
            string body = reader.ReadToEnd();

            //TY for the concistent response avans
            var uri = body.Replace("php&", "php?");
            uri = uri.Split("authentification_url=")[1];

            return AvansOauthHelper.getTokenFormUri(uri);
        }

        public static OauthToken GetAccesToken(AvansOauthHelperOptions options, OauthToken requestToken,  string verifier)
        {
           
            // Creating a new instance with a helper method
            OAuthRequest client = OAuthRequest.ForAccessToken(options.AvansClientId, options.AvansSecret, requestToken.Token, requestToken.Secret);
            client.Verifier = verifier;
            client.RequestUrl = "https://publicapi.avans.nl/oauth/access_token";

            // Using URL query authorization to get the request token
            string auth = client.GetAuthorizationQuery();
            var url = client.RequestUrl + "?" + auth;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            Stream receiveStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
            string body = reader.ReadToEnd();

            //turn body into uri
            var uri = "http://temp?" + body;

            return AvansOauthHelper.getTokenFormUri(uri);
        }


        public static string GetUserInfo(AvansOauthHelperOptions options, OauthToken accesToken)
        {
            OAuthRequest client = OAuthRequest.ForProtectedResource("GET", options.AvansClientId, options.AvansSecret, accesToken.Token, accesToken.Secret);
            client.RequestUrl = "https://publicapi.avans.nl/oauth/people/@me";

            // Using URL query authorization to get the request token
            string auth = client.GetAuthorizationQuery();
            var url = client.RequestUrl + "?" + auth;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            Stream receiveStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
            string body = reader.ReadToEnd();

            return body;
        }

        public static OauthToken getTokenFormUri(string uri)
        {
            Uri myUri = new Uri(uri);

            return new OauthToken()
            {
                Token = HttpUtility.ParseQueryString(myUri.Query).Get("oauth_token"),
                Secret= HttpUtility.ParseQueryString(myUri.Query).Get("oauth_token_secret")
            };

        }
    }

    public class AvansOauthHelperOptions
    {
        public string AvansSecret { get; set; }
        public string AvansClientId { get; set; }
    }

    public class OauthToken
    {
        public string Token { get; set; }

        public string Secret { get; set; }
    }
}
