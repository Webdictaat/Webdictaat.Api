using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

namespace Webdictaat.Api.Test.Controller
{
    public class QuestionControllerTest : BaseTestController
    {
        private QuestionsController _controller;
        private IQuestionRepository _repo;
        private UserManager<ApplicationUser> _userManager;

        public QuestionControllerTest()
        {
          
        }

        [Fact]
        public void Should_Create()
        {

        }

        [Fact]
        public void Should_Update()
        {
          


        }

        [Fact]
        public void Should_Delete()
        {
           
        }


    }
}



    