using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Webdictaat.Api.Models;
using Webdictaat.Api.Services;
using Webdictaat.Core;
using Webdictaat.Data;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.Test.Controller
{
    public class BaseTestController
    {
        public ParticipantRepository participantRepository { get; set; }
        public UserManager<ApplicationUser> userManager { get; set; }

        public BaseTestController()
        {

            //participantRepository = new ParticipantRepository(um);
        }
    }
}