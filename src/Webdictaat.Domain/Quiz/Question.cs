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
        public virtual ICollection<Answer> Answers { get; set; }

        public Question()
        {
            Answers = new List<Answer>();
        }
    }
}
