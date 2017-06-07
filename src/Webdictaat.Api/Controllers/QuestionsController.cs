using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.Api.Models;
using Webdictaat.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webdictaat.Api.Controllers
{
    /// <summary>
    /// Sub route of quiz
    /// </summary>
    [Route("api/dictaten/{dictaatName}/quiz/{quizId}/[controller]")]
    public class QuestionsController : Controller
    {
        private IQuestionRepository _questionRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionRepo"></param>
        public QuestionsController(IQuestionRepository questionRepo)
        {
            _questionRepo = questionRepo;
        }


        /// <summary>
        /// Authorized (Requires the user to be logged in.)
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="question"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public QuestionVM Post(string dictaatName, [FromBody]QuestionVM question)
        {
            QuestionVM result = _questionRepo.CreateQuestion(question);
            return result;
        }

        [Authorize]
        [HttpPut("{questionId}")]
        public QuestionVM Put(string dictaatName, int questionId, [FromBody]QuestionVM question)
        {
            QuestionVM result = _questionRepo.UpdateQuestion(question);
            return result;
        }

        [Authorize]
        [HttpDelete("{questionId}")]
        public QuestionVM Delete(string dictaatName, int questionId)
        {
            QuestionVM result = _questionRepo.DeleteQuestion(questionId);
            return result;
        }
    }
}
