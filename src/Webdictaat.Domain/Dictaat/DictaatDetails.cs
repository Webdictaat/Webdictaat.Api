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

        public ICollection<string> GetContributersIds()
        {
            return new string[] { this.DictaatOwnerId };
        }
    }
}
