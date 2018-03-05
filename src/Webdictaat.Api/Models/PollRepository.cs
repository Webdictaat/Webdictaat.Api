using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Data;
using Webdictaat.Domain;

namespace Webdictaat.Api.Models
{
    public interface IPollRepository
    {
        PollVM GetPoll(int pollId, string userId = null);
        IEnumerable<PollVM> GetPolls(string dictaatName);
        PollVM CreatePoll(string dictaatName, PollVM poll);
        PollVM UpdatePoll(string dictaatName, int pollId, PollVM poll);
        PollVM Vote(string dictaatName, int pollId, int optionId, string userId);
    }

    public class PollRepository : IPollRepository
    {
        private WebdictaatContext _context;

        public PollRepository(
            WebdictaatContext context)
        {
            _context = context;
        }

        public PollVM CreatePoll(string dictaatName, PollVM poll)
        {
            Poll p = new Poll();
            p.Question = poll.Question;
            p.DictaatName = dictaatName;
            p.Options = poll.Options.Select(o => new PollOption()
            {
                Text = o.Text
            }).ToList();

            _context.Polls.Add(p);
            _context.SaveChanges();
            return new PollVM(p);
        }

        public PollVM GetPoll(int pollId, string userId = null)
        {
            Poll p = _context.Polls
              .Include("Options.Votes")
              .Include("Votes")
              .FirstOrDefault(q => q.Id == pollId);

            if (p == null)
                return null;

            return new PollVM(p, userId);
        }

        public IEnumerable<PollVM> GetPolls(string dictaatName)
        {
            return _context.Polls
                    .Where(p => p.DictaatName == dictaatName)
                    .ToList()
                    .Select(p => new PollVM(p));
        }

        public PollVM UpdatePoll(string dictaatName, int pollId, PollVM newPoll)
        {
            Poll oldPoll = _context.Polls
            .Include("Options")
            .FirstOrDefault(q => q.Id == pollId);

            oldPoll.Question = newPoll.Question;

            var options = oldPoll.Options;

            //remove
            oldPoll.Options.ToList().ForEach(o =>
            {
                if (!newPoll.Options.Any(npo => npo.Id == o.Id))
                    options.Remove(o);
            });


            //add op update
            newPoll.Options.ToList().ForEach(no =>
            {
                if (no.Id != 0)
                    options.FirstOrDefault(o => o.Id == no.Id).Text = no.Text; 
                else
                    options.Add(new PollOption() { Text = no.Text });
            });

            oldPoll.Options = options;
            _context.SaveChanges();
            return new PollVM(oldPoll);
        }

        public PollVM Vote(string dictaatName, int pollId, int optionId, string userId)
        {
            Poll p = _context.Polls
              .Include("Options.Votes")
              .Include("Votes")
              .FirstOrDefault(q => q.Id == pollId);

            if (p == null)
                return null;

            if (p.Votes.Any(v => v.UserId == userId))
                return new PollVM(p, userId);

            p.Votes.Add(new PollVote()
            {
                PollOptionId = optionId,
                UserId = userId
            });

            _context.SaveChanges();

            return new PollVM(p, userId);
        }
    }
}
