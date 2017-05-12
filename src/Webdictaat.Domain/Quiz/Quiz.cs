using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string DictaatDetailsName { get; set; }

        public DateTime Timestamp { get; set; }

        public virtual ICollection<QuestionQuiz> Questions { get; set; }

        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }

        public Quiz()
        {

        }

    }
}
