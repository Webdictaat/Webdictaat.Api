using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Webdictaat.Domain
{
    public class PollOption
    {
        [Key]
        public int Id { get; set; }

        public int PollId {get; set;}

        [ForeignKey("PollId")]
        public Poll Poll { get; set; }

        public string Text { get; set; }

        public virtual ICollection<PollVote> Votes { get; set; }

    }
}
