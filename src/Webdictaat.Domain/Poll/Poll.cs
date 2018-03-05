using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Webdictaat.Domain
{
    public class Poll
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// DEPRECATED - Replaced by Body
        /// </summary>
        public virtual ICollection<PollOption> Options { get; set; }

        public virtual ICollection<PollVote> Votes { get; set; }

        [Required]
        public string DictaatName { get; set; }

        public string Question { get; set; }
        public bool IsDeleted { get; set; }
    }
}
