using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class QuestionAttemptVM
    {
        public int QuestionId { get; set; }

        public Boolean IsCorrect { get; set; }

        public QuestionAttemptVM()
        {

        }

        public QuestionAttemptVM(QuizAttemptQuestion qaq)
        {
            this.QuestionId = qaq.QuestionId;
            this.IsCorrect = qaq.IsCorrect;
        }


        public QuizAttemptQuestion ToPoco()
        {
            return new QuizAttemptQuestion()
            {
                QuestionId = this.QuestionId,
                IsCorrect = this.IsCorrect
            };
        }
    }
}
