using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class QuizAttemptVM
    {
      

        public QuizAttemptVM(QuizAttempt qa)
        {
            this.QuizId = qa.QuizId;
            this.Timestamp = qa.Timestamp;
            this.CorrectAnswers = qa.Answers.Where(a => a.Answer.IsCorrect).Count();
            this.AnswerIs = qa.Answers.Select(a => a.AnswerId).ToList();
        }

        public int QuizId { get; set; }

        public DateTime Timestamp { get; set; }

        public ICollection<int> AnswerIs { get; set; }

        public int CorrectAnswers { get; set; }
    }
}
