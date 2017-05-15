using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Webdictaat.Data;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.Test.Controller
{
    public class BaseTestController
    {
        protected WebdictaatContext _context;
        protected Mock<IUserStore<ApplicationUser>> _store; 

        public BaseTestController()
        {
            var builder = new DbContextOptionsBuilder<WebdictaatContext>().UseInMemoryDatabase();
            _context = new WebdictaatContext(builder.Options);
            _store = new Mock<IUserStore<ApplicationUser>>(MockBehavior.Strict);
        }
    }
}