using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class PollOptionVM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int VotesCount { get; set; }
        public int VotesPercentage { get; set; }

        public PollOptionVM()
        {

        }

        public PollOptionVM(PollOption option, Poll poll)
        {
            this.Id = option.Id;
            this.Text = option.Text;

            if(option.Votes != null)
            {
                this.VotesCount = option.Votes.Count();

                if (this.VotesCount != 0)
                {
                    this.VotesPercentage = 100 / (poll.Votes.Count() / this.VotesCount);
                }
            }

            
        }
    }
}
