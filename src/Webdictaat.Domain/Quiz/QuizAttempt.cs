using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    
    public class QuizAttemptQuestion
    {
        [ForeignKey("QuizAttemptId")]
        public virtual QuizAttempt QuizAttempt { get; set; }

        public int QuizAttemptId { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question {get; set; }

        public int QuestionId { get; set; }

        public Boolean IsCorrect { get; set; }

        public QuizAttemptQuestion()
        {

        }
    }

    public class QuizAttempt
    {
        public int Id { get; set; }

        public string UserId { get; set; } 

        public DateTime Timestamp { get; set; }

        public virtual ICollection<QuizAttemptQuestion> QuestionsAnswered { get; set; }


        public int QuizId { get; set; }

        public QuizAttempt()
        {
            this.QuestionsAnswered = new List<QuizAttemptQuestion>();
        }
    }
}
