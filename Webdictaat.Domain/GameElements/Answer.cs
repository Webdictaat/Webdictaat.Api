using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webdictaat.Domain
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        
        public bool IsCorrect { get; set; }

        [Required]
        public int QuestionId { get; set; }
 
    }
}