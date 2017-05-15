using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webdictaat.Domain
{
    /// <summary>
    /// Naamgeving automatisch op answer, i.p.v answers
    /// </summary>
    [Table("Answer")]
    public class Answer
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        
        public bool IsCorrect { get; set; }

        [Required]
        public int QuestionId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<QuizAttemptAnswer> QuizAttempts { get; set; }
        
        public Answer()
        {
            this.QuizAttempts = new List<QuizAttemptAnswer>();
        }

    }
}