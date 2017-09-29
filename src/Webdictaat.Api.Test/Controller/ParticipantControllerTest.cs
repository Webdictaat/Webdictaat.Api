using System;
using System.Collections.Generic;
using System.Text;
using Webdictaat.Api.Controllers;
using Xunit;

namespace Webdictaat.Api.Test.Controller
{
    class ParticipantControllerTest : BaseTestController
    {
        [Fact]
        public void Should_Get_Participant()
        {
            //arrange
            var ctrl = new ParticipantController(participantRepository, userManager, null);
            //act



            //assert

        }
    }
}
