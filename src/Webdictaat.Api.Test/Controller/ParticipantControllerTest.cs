using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Webdictaat.Api.Controllers;
using Webdictaat.Api.ViewModels;
using Webdictaat.Api.ViewModels.Participant;
using Xunit;


namespace Webdictaat.Api.Test.Controller
{
    public class ParticipantControllerTest : BaseTestController
    {
        Webdictaat.Api.Controllers.ParticipantController _c;

        public ParticipantControllerTest()
        {
            _context.Database.BeginTransaction();

            _c = new ParticipantController(_participantRepo, null, base.am.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new TestPrincipal(new Claim[]{
                            new Claim("name", "ssmulder")
                        })
                    }
                }
            };
        }

        [Fact]
        public void Should_Get_Participants_comform_AVG()
        {
            //ARRANGE
            //ACT
            List<UserVM> result = _c.GetParticipants("Test").ToList();

            //ASSERT
            Assert.DoesNotContain(result, p => p.Email != null);
        }

        [Fact]
        public void Should_Get_Participant_with_email()
        {
            //ARRANGE
            //ACT
            ParticipantVM result = _c.GetParticipant("Test", "linksonder@gmail.com");

            //ASSERT
            Assert.NotNull(result.Email);
        }
    }
}
