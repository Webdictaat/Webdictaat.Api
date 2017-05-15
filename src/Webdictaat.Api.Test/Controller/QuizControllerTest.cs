using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.CMS.Controllers;
using Webdictaat.CMS.Models;
using Webdictaat.CMS.ViewModels;
using Webdictaat.Data;
using Webdictaat.Domain;
using Webdictaat.Domain.User;
using Xunit;

namespace Webdictaat.Api.Test.Controller
{
    public class QuizControllerTest : BaseTestController
    {
        private QuizController _controller;
        private QuizRepository _repo;
        private UserManager<ApplicationUser> _userManager;

        public QuizControllerTest()
        {
            _repo = new QuizRepository(new QuestionRepository(_context), _context);
            _userManager = new UserManager<ApplicationUser>(_store.Object, null, null, null, null, null, null, null, null);
            _controller = new QuizController(_repo, _userManager);
        }

        [Fact]
        public async void Should_Update()
        {
            //arrange
            TestDatabase.CreateQuizes(_context);
            TestDatabase.CreateQuestions(_context);

            var form = new QuizVM()
            {
                Id = 11,
                Title = "Title-e",
                Description = "desc-e",
                Questions = new List<QuestionVM>()
                {
                    new QuestionVM() {Id = 11, Text = "Q1", Answers = new List<AnswerVM>(){ //still the same
                        new AnswerVM() { Id = 11, Text = "Q1-A1" }, new AnswerVM() { Id = 12, Text = "Q1-A2", IsCorrect = true }
                    }},
                    new QuestionVM() {Id = 12, Text = "Q2-e", Answers = new List<AnswerVM>(){ //edit
                        new AnswerVM() { Id = 13, Text = "Q2-A1", IsCorrect = true  }, new AnswerVM() { Id = 14, Text = "Q1-A2"}
                    }},
                    new QuestionVM() {Text = "Q3-e", Answers = new List<AnswerVM>(){ //new
                        new AnswerVM() { Text = "Q3-A1", IsCorrect = true  }, new AnswerVM() { Text = "Q3-A2"}
                    }},
                }
            };


            //act
            var response =  _controller.Put("test", form);

            //assert
            Assert.Equal("Title-e", response.Title);
            Assert.Equal("desc-e", response.Description);
            Assert.Equal(3, response.Questions.Count());
            Assert.Equal("Q1", response.Questions.FirstOrDefault(q => q.Id == 11).Text);
            Assert.Equal("Q2-e", response.Questions.FirstOrDefault(q => q.Id == 12).Text);
            Assert.Equal(true, response.Questions.FirstOrDefault(q => q.Id == 12)
                .Answers.FirstOrDefault(a => a.Id == 13).IsCorrect);
            Assert.Equal("Q3-e", response.Questions.FirstOrDefault(q => q.Id == 2).Text);
        }


    }
}



    