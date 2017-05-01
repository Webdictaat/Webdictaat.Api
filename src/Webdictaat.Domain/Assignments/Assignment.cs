using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain.Assignments
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string DictaatDetailsName { get; set; }

        public string Metadata { get; set; }

        public string AssignmentSecret { get; set; }

        public int Points { get; set; }

        public virtual ICollection<AssignmentSubmission> Attempts { get; set; }
    }
}
