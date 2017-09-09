using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.Api.Models;
using Webdictaat.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Webdictaat.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Webdictaat.Domain.User;
using Webdictaat.Domain;
using Webdictaat.Api.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webdictaat.Api.Controllers
{

    [Route("api/dictaten/{dictaatName}/[controller]")]
    public class QuizController : BaseController
    {
        private IQuizRepository _quizRepo;
        private IAuthorizeService _authorizeService;
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="quizRepo"></param>
        /// <param name="userManager"></param>
        /// <param name="authorizeService"></param>
        public QuizController(
            IQuizRepository quizRepo, 
            UserManager<ApplicationUser> userManager,
            IAuthorizeService authorizeService) : base(authorizeService)
        {
            _userManager = userManager;
            _quizRepo = quizRepo;
            _authorizeService = authorizeService;
    }

        /// <summary>
        /// Returns a list of quizes 
        /// </summary>
        /// <param name="dictaatName"></param> 
        /// <returns></returns>
        [HttpGet]
        public ICollection<QuizSummaryVM> Get(string dictaatName)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            ICollection<QuizSummaryVM> result = _quizRepo.GetQuizes(dictaatName, userId);
            return result;
        }

        [HttpGet("{quizId}")]
        [Authorize]
        public QuizVM Get(string dictaatName, int quizId)
        {
            var user = _userManager.GetUserId(HttpContext.User);
            QuizVM result = _quizRepo.GetQuiz(quizId, user);
            return result;
        }

        /// <summary>
        /// Authorized (Requires the user to be logged in.)
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="quiz"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost] 
        public async Task<QuizVM> Post(string dictaatName, [FromBody]QuizVM quiz)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            QuizVM result = _quizRepo.CreateQuiz(dictaatName, quiz);
            return result;
        }

        /// <summary>
        /// Authorized (Requires the user to be logged in.)
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="quiz"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{quizId}")]
        public async Task<QuizVM> Put(string dictaatName, [FromBody]QuizVM quiz)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            QuizVM result = _quizRepo.UpdateQuiz(dictaatName, quiz);
            return result;
        }

        /// <summary>
        /// Authorized (Requires the user to be logged in.)
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="quizId"></param>
        /// <param name="quizAttempt"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{quizId}/Attempts")]
        public Api.ViewModels.QuizAttemptVM Post(string dictaatName, int quizId, [FromBody]QuizAttemptForm quiz)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            Api.ViewModels.QuizAttemptVM result = _quizRepo.AddAttempt(quizId, userId, quiz.GivenAnswers);
            return result;
        }
    }
}
