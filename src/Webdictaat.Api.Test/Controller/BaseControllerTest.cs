using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Api.Controllers;
using Webdictaat.Api.Models;
using Webdictaat.Data;
using Webdictaat.Domain;
using Webdictaat.Domain.User;
using Xunit;
using Webdictaat.Api.Services;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.Core;
using Moq;

namespace Webdictaat.Api.Test.Controller
{
    public class BaseControllerTest : BaseTestController, IDisposable
    {
        Webdictaat.Api.Controllers.BaseController _c;

        public BaseControllerTest()
        {
            _context.Database.BeginTransaction();

            _c = new BaseController(base.am.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = _user
                    }
                }
            };
        }

        [Fact]
        public void Should_AuthorizeResource_When_Only_Owner()
        {
            //arrange
            base.am.Setup(a => a.isDictaatOwner(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            base.am.Setup(a => a.IsDictaatContributer(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            //act
            var response = _c.AuthorizeResource("notrelevant");

            //assert
            Assert.True(response);
        }

        [Fact]
        public void Should_AuthorizeResource_When_Only_Contributer()
        {
            //arrange
            base.am.Setup(a => a.isDictaatOwner(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            base.am.Setup(a => a.IsDictaatContributer(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            //act
            var response = _c.AuthorizeResource("notrelevant");

            //assert
            Assert.True(response);
        }

        [Fact]
        public void Should_Not_AuthorizeResource_When_Not_Allowed()
        {
            //arrange
            base.am.Setup(a => a.isDictaatOwner(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            base.am.Setup(a => a.IsDictaatContributer(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            //act
            var response = _c.AuthorizeResource("notrelevant");

            //assert
            Assert.False(response);
        }

        public void Dispose()
        {
            _context.Database.RollbackTransaction();
        }
    }
}
