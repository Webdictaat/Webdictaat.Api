using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    //koppeltabel
    public class QuizAttemptAnswer
    {
        [ForeignKey("QuizAttemptId")]
        public virtual QuizAttempt QuizAttempt { get; set; }

        public int QuizAttemptId { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answer Answer {get; set; }

        public int AnswerId { get; set; }

        public QuizAttemptAnswer()
        {

        }
    }


    public class QuizAttempt
    {
        public int Id { get; set; }

        public string UserId { get; set; } 

        public DateTime Timestamp { get; set; }

        public virtual ICollection<QuizAttemptAnswer> Answers { get; set; }

        public int QuizId { get; set; }

        public QuizAttempt()
        {
            this.Answers = new List<QuizAttemptAnswer>();
        }
    }
}
