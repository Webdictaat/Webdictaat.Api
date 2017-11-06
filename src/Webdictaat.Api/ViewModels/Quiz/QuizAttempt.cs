using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class QuizAttemptVM
    {
        public QuizAttemptVM()
        {
            this.QuestionsAnswered = new List<QuestionAttemptVM>();
        }
      

        public QuizAttemptVM(QuizAttempt qa)
        {
            this.QuizId = qa.QuizId;
            this.Timestamp = qa.Timestamp;
            this.QuestionsAnswered = qa.QuestionsAnswered.Select(a => new QuestionAttemptVM(a));
        }

        public int QuizId { get; set; }

        public DateTime Timestamp { get; set; }

        public IEnumerable<QuestionAttemptVM> QuestionsAnswered { get; set; }

        public int CorrectAnswers { get; set; }
    }
}
