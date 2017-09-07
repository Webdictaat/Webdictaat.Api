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
            _repo = new QuestionRepository(_context);
            //_userManager = new UserManager<ApplicationUser>(_store.Object, null, null, null, null, null, null, null, null);
            _controller = new QuestionsController(_repo);
        }

        [Fact]
        public void Should_Update()
        {
            //arrange
            TestDatabase.CreateQuestions(_context);

            var form = new QuestionVM()
            {
                Id = 11,
                Text = "Q1-e",
                Answers = new List<AnswerVM>()
                {
                    new AnswerVM() { Id = 11 ,Text = "niet relevant", IsCorrect = true },
                    new AnswerVM() { Text = "Q1-A3"},
                }
            };


            //act
            var response = _controller.Put("test", 11, form);

            //assert
            Assert.Equal("Q1-e", response.Text);
            Assert.Equal(2, response.Answers.Count());
            Assert.Equal("Q1-A1", response.Answers.FirstOrDefault(a => a.Id == 11).Text);
            Assert.Equal(true, response.Answers.FirstOrDefault(a => a.Id == 11).IsCorrect);
            Assert.Equal("Q1-A3", response.Answers.FirstOrDefault(a => a.Id == 1).Text);
        }

        [Fact]
        public void Should_Delete()
        {
            //arrange
            TestDatabase.CreateQuestions(_context);

            var form = new QuestionVM()
            {
                Id = 11,
            };


            //act
            var response = _controller.Delete("test", 11);

            //assert
            Assert.Equal(true, response.IsDeleted);
        }


    }
}



    