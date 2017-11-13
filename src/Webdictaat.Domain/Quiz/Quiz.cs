using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.Assignments;

namespace Webdictaat.Domain
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// DEPRECATED - Replaced by assignment title
        /// No longer required
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// DEPRECATED - Replaced by assignment descritpion
        /// No longer required
        /// </summary>
        public string Description { get; set; }

        [Required]
        public string DictaatDetailsName { get; set; }

        public DateTime Timestamp { get; set; }
        
        public Boolean Shuffle { get; set; }

        public int? AssignmentId { get; set; }

        [ForeignKey("AssignmentId")]
        public virtual Assignment Assignment { get; set; }

        public virtual ICollection<QuestionQuiz> Questions { get; set; }

        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }

        public Quiz()
        {

        }

    }
}
