using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class PollVM
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public IEnumerable<PollOptionVM> Options { get; set; }

        public PollOptionVM MyVote { get; set; }

        public PollVM()
        {
            Options = new List<PollOptionVM>();
        }

        public PollVM(Poll p, string userId = null)
        {
            this.Id = p.Id;
            this.Question = p.Question;

            if(p.Options != null)
                this.Options = p.Options.Select(o => new PollOptionVM(o, p));

            if(userId != null)
            {
                var vote = p.Votes.FirstOrDefault(v => v.UserId == userId);
                this.MyVote = vote != null ? new PollOptionVM(vote.PollOption, p) : null;
            }
        }
    }
}
