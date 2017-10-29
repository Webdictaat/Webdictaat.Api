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
    public class QuizControllerTest : BaseTestController
    {
        private QuizController _controller;
        private QuizRepository _quizRepository;
        private QuestionRepository _questionRepo;
     

        public QuizControllerTest()
        {
            _questionRepo = new QuestionRepository(_context);
            _quizRepository = new QuizRepository(_questionRepo, _context);
            _controller = new QuizController(_quizRepository, umm.Object, am.Object);
        }

        [Fact]
        public async void Should_Create()
        {
            //arrange
            var vm = new QuizVM();
            vm.Title = "Test Quiz";

            //act
            var quiz = await _controller.Post("a", vm);

            //assert
            _quizRepository.GetQuiz(quiz.Id);
            Assert.Equal(vm.Title, quiz.Title);
        }

        [Fact]
        public async void Should_Update()
        {
           
        }


    }
}



    