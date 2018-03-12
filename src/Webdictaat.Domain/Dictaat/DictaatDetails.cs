using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.Assignments;
using Webdictaat.Domain.User;

namespace Webdictaat.Domain
{
    public class DictaatDetails : IResource
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public string DictaatOwnerId { get; set; }

        public ApplicationUser DictaatOwner { get; set; }

        public bool IsEnabled { get; set; }

        public virtual ICollection<DictaatContributer> Contributers { get; set; }

        public virtual ICollection<DictaatAchievement> Achievements { get; set; }

        public virtual ICollection<DictaatSession> Sessions { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual ICollection<Quiz> Quizes { get; set; }

        public virtual ICollection<DictaatGroup> Groups { get; set; }

        public virtual ICollection<Poll> Polls { get; set; }

        public DictaatDetails()
        {
            this.Quizes = new List<Quiz>();
            this.Assignments = new List<Assignment>();
            this.Sessions = new List<DictaatSession>();
            this.Polls = new List<Poll>();
        }


        public ICollection<string> GetContributersIds()
        {
            var result = new List<string>{ this.DictaatOwnerId };
            if (this.Contributers != null)
            {
                this.Contributers.ToList().ForEach(c => result.Add(c.UserId));
            }
            return result;
        }
    }
}
