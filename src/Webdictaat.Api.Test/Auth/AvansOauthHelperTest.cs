using System;
using System.Collections.Generic;
using System.Text;
using Webdictaat.Api.Auth;
using Xunit;

namespace Webdictaat.Api.Test.Auth
{
    public class AvansOauthHelperTest
    {
        [Fact]
        public void getTokenFromResponse_Should_Return_Token()
        {
            //arrange
            //act
            OauthToken token = AvansOauthHelper.getTokenFormUri("authentification_url=https://publicapi.avans.nl/oauth/login.php&oauth_token=oauthtoken&oauth_token_secret=oauthsecret&oauth_callback_confirmed=true");

            //assert
            Assert.Equal("oauthtoken", token.Token);

        }
    }
}
