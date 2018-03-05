using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Webdictaat.Domain
{
    [Table("PollVotes")]
    public class PollVote
    {
        //concat id
        public string UserId { get; set; }

        public int PollId { get; set; }
        [ForeignKey("PollId")]
        public Poll Poll { get; set; }


        public int PollOptionId { get; set; }
        [ForeignKey("PollOptionId")]
        public PollOption PollOption { get; set; }
    }
}
