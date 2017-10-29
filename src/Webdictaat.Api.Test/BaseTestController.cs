using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Webdictaat.Api.Models;
using Webdictaat.Api.Services;
using Webdictaat.Core;
using Webdictaat.Data;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.Test.Controller
{
    public class BaseTestController
    {
        protected Mock<ParticipantRepository> prm { get; set; }
        protected Mock<UserManager<ApplicationUser>> umm { get; set; }
        protected Mock<IAuthorizeService> am { get; set; }

        protected WebdictaatContext _context;

        public BaseTestController()
        {
            am = new Mock<IAuthorizeService>();
            prm = new Mock<ParticipantRepository>();
            umm = new Mock<UserManager<ApplicationUser>>();

            _context = new WebdictaatContext(new DbContextOptions<WebdictaatContext>());
        }
    }
}