using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    public class QuestionQuiz
    {
  
        public QuestionQuiz(Question question)
        {
            this.Question = question;
        }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        public int QuestionId { get; set; }

        [ForeignKey("QuizId")]
        public Quiz Quiz { get; set; }

        public int QuizId { get; set; }

        public QuestionQuiz()
        {

        }
    }

    public class Question
    {
        [Key]   
        public int Id { get; set; }

        public virtual ICollection<QuestionQuiz> Quizes { get; set; }

        public string Text { get; set; }
        public bool IsDeleted { get; set; }

        public string QuestionType { get; set; }

        /// <summary>
        /// A JSON string
        /// </summary>
        public string Body { get; set; }

        public virtual ICollection<QuizAttemptQuestion> Attempts { get; set; }


        public Question()
        {

        }
    }
}
