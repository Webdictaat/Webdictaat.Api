using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain.Assignments
{
    public enum AssignmentLevel
    {
        Bronze = 0, Silver = 1, Gold = 2
    }

    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        [ForeignKey("DictaatDetailsId")]
        public virtual DictaatDetails DictaatDetails { get; set; }


        [Required]
        public string DictaatDetailsId { get; set; }

        public string Metadata { get; set; }

        public string AssignmentSecret { get; set; }

        /// <summary>
        /// A refrence to an external assignmnet or resource linked to this assignment.
        /// </summary>
        public string ExternalId { get; set; }

        public AssignmentLevel Level { get; set; }

        public int Points { get; set; }

        public virtual ICollection<AssignmentSubmission> Attempts { get; set; }
    }
}
