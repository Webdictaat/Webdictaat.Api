using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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

        public virtual ICollection<DictaatContributer> Contributers { get; set; }

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
