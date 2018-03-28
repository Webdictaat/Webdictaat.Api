using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using Webdictaat.Api.Models;
using Webdictaat.Api.Services;
using Webdictaat.Core;
using Webdictaat.Data;
using Webdictaat.Domain;
using Webdictaat.Domain.Google;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.Test.Controller
{
    public class TestPrincipal : ClaimsPrincipal
    {
        public TestPrincipal(params Claim[] claims) : base(new TestIdentity(claims))
        {
        }
    }

    public class TestIdentity : ClaimsIdentity
    {
        public TestIdentity(params Claim[] claims) : base(claims)
        {
        }
    }

    public class BaseTestController
    {
        protected Mock<ParticipantRepository> prm { get; set; }
        protected Mock<UserManager<ApplicationUser>> umm { get; set; }
        protected Mock<IAuthorizeService> am { get; set; }

        protected Mock<IOptions<ConfigVariables>> _config { get; set; }

        protected WebdictaatContext _context;


        #region Repos 
        protected DictaatRepository _dictaatRepo;
        #endregion

        protected ClaimsPrincipal _user;

        protected Mock<IDictaatFactory> _dictaatFactory;
        private Mock<IGoogleAnalytics> _analytics;


        public BaseTestController()
        {
            am = new Mock<IAuthorizeService>();
            am.Setup(a => a.isDictaatOwner(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));
            am.Setup(a => a.IsDictaatContributer(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            prm = new Mock<ParticipantRepository>();
            umm = new Mock<UserManager<ApplicationUser>>();

            _user = new TestPrincipal(new Claim[]{
                new Claim("Name", "ssmulder")
            });

            _config = new Mock<IOptions<ConfigVariables>>();
            ConfigVariables vars = new ConfigVariables()
            {
                DictaatRoot = "//resources",
                PagesDirectory = "pages",
                DictaatConfigName = "dictaat.config.json",
                DictatenDirectory = "dictaten",
                TemplatesDirectory = "templates",
                MenuConfigName = "nav-menu.json",
                ImagesDirectory = "images",
                StyleDirectory = "styles",
            };
            _config.Setup(c => c.Value).Returns(vars);

            //database
            var options = new DbContextOptionsBuilder<WebdictaatContext>()
                .UseSqlServer("Data Source=(localdb)\\webdictaat;Initial Catalog=webdictaat;Integrated Security=False;User ID=ssmulder;Password=password;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .Options;
            _context = new WebdictaatContext(options);

            //managers and factories
            _dictaatFactory = new Mock<IDictaatFactory>();
            _dictaatFactory.Setup(df => df.GetDictaat(It.IsAny<string>())).Returns(new Domain.Dictaat(){Name = "Test"});
            _dictaatFactory.Setup(df => df.CopyDictaat(It.IsAny<string>(), It.IsAny<DictaatDetails>())).Returns(new Domain.Dictaat() { Name = "Test2" });
            _analytics = new Mock<IGoogleAnalytics>(); 

            //repos
            _dictaatRepo = new DictaatRepository(_config.Object, _analytics.Object, _dictaatFactory.Object, _context);
        }
    }
}