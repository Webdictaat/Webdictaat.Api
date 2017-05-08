using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.User;

namespace Webdictaat.Domain
{
    //koppeltabel
    public class DictaatContributer
    {
        [ForeignKey("DictaatDetailsId")]
        public virtual DictaatDetails DictaatDetails { get; set; }

        public string DictaatDetailsId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
