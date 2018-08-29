using System;
using System.Collections.Generic;
using System.Text;
using Webdictaat.Api.ViewModels;
using Xunit;

namespace Webdictaat.Api.Test.ViewModels
{

    public class IssuePercentage_VotesAreInt
    {
        [Fact]
        public void PercentageIsCorrect()
        {
            //arrange
            var option = new Domain.PollOption()
            {
                Votes = new List<Domain.PollVote>()
                {
                    new Domain.PollVote(),
                    new Domain.PollVote()
                }
            };
            var poll = new Domain.Poll()
            {
                Votes = new List<Domain.PollVote>()
                {
                    new Domain.PollVote(),
                    new Domain.PollVote(),
                    new Domain.PollVote()
                }
            };

            //act
            var vm = new PollOptionVM(option, poll);

            //assert
            Assert.Equal(66, vm.VotesPercentage);
        }
    }
}
