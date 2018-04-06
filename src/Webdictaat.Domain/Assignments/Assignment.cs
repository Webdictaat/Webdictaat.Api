using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain.Assignments
{
    public enum AssignmentType
    {
        Open = 0, Quiz = 1, External = 2
    }

    public enum AssignmentLevel
    {
        Bronze = 0, Silver = 1, Gold = 2
    }

    public class Assignment
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string DictaatDetailsId { get; set; }


        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }


        [ForeignKey("DictaatDetailsId")]
        public virtual DictaatDetails DictaatDetails { get; set; }



        public string Metadata { get; set; }

        public string AssignmentSecret { get; set; }

        public AssignmentType? AssignmentType { get; set; }

        /// <summary>
        /// A refrence to an external assignmnet or resource linked to this assignment.
        /// </summary>
        public string ExternalId { get; set; }

        public AssignmentLevel Level { get; set; }

        public int Points { get; set; }

        public virtual ICollection<AssignmentSubmission> Attempts { get; set; }

        public Assignment Copy(string newName)
        {
            return new Assignment()
            {
                DictaatDetailsId = newName,
                Description = this.Description,
                AssignmentSecret = this.AssignmentSecret,
                AssignmentType = this.AssignmentType,
                ExternalId = this.ExternalId,
                Level = this.Level,
                Metadata = this.Metadata,
                Points = this.Points,
                Title = this.Title
            };
        }
    }
}
