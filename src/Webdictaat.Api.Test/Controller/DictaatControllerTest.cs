using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Api.Controllers;
using Webdictaat.Api.Models;
using Webdictaat.Api.ViewModels;
using Webdictaat.Data;
using Webdictaat.Domain;
using Webdictaat.Domain.User;
using Xunit;
using Webdictaat.Api.Services;

namespace Webdictaat.Api.Test.Controller
{
    public class DictaatControllerTest : BaseTestController
    {
        private DictatenController _controller;
        private IDictaatRepository _repo;
        private UserManager<ApplicationUser> _userManager;
     

        public DictaatControllerTest()
        {
            //_repo = new DictaatRepository(_context, _file, _dir, _json, _context);
            //_userManager = new UserManager<ApplicationUser>(_store.Object, null, null, null, null, null, null, null, null);
            //_controller = new DictatenController(_repo, _userManager, _authService, _context)
        }

        [Fact]
        public void Should_Get_Participants()
        {
           
        }

    }
}



    