using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.Api.Models;
using Webdictaat.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Webdictaat.Api.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webdictaat.Api.Controllers
{
    /// <summary>
    /// Sub route of quiz
    /// </summary>
    [Route("api/dictaten/{dictaatName}/quiz/{quizId}/[controller]")]
    public class QuestionsController :BaseController
    {
        private IQuestionRepository _questionRepo;
        private IAuthorizeService _authorizeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionRepo"></param>
        public QuestionsController(
            IQuestionRepository questionRepo, 
            IAuthorizeService authorizeService) : base(authorizeService)
        {
            _questionRepo = questionRepo;
            _authorizeService = authorizeService;
        }


        /// <summary>
        /// Create Question
        /// Authorized (Requires the user to be logged in.)
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public QuestionVM Post(string dictaatName, [FromBody]QuestionVM question)
        {
            if (!AuthorizeResource(dictaatName))
                return null;

            QuestionVM result = _questionRepo.CreateQuestion(question);
            return result;
        }

        /// <summary>
        /// Update Question
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="questionId"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{questionId}")]
        public async Task<QuestionVM> Put(string dictaatName, int questionId, [FromBody]QuestionVM question)
        {
            if (!AuthorizeResource(dictaatName))
                return null;
            QuestionVM result = _questionRepo.UpdateQuestion(question);
            return result;
        }

        /// <summary>
        /// Delete question
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="questionId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{questionId}")]
        public QuestionVM Delete(string dictaatName, int questionId)
        {
            if (!AuthorizeResource(dictaatName))
                return null;

            QuestionVM result = _questionRepo.DeleteQuestion(questionId);
            return result;
        }
    }
}
