using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain.User
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<DictaatDetails> OwnedDictaten { get; set; }

        public ICollection<DictaatContributer> ContributedDictaten { get; set; }
    }
}
