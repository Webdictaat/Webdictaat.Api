using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Webdictaat.Api.Services;
using Webdictaat.Core;
using Webdictaat.Data;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.Test.Controller
{
    public class BaseTestController
    {
        protected Mock<IAuthorizeService> _authService;
        protected WebdictaatContext _context;
        protected Mock<IUserStore<ApplicationUser>> _store;

        //core
        protected Mock<IFile> _file;
        protected Mock<IDirectory> _dir;
        protected Mock<IJson> _json;

        public BaseTestController()
        {
            var builder = new DbContextOptionsBuilder<WebdictaatContext>().UseInMemoryDatabase();
            _context = new WebdictaatContext(builder.Options);
            _store = new Mock<IUserStore<ApplicationUser>>(MockBehavior.Strict);
            _authService = new Mock<IAuthorizeService>();
        }
    }
}