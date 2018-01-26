using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Webdictaat.Domain
{
    public class DictaatGroup
    {
        public string Name { get; set; }

        public string DictaatName { get; set; }

        [ForeignKey("DictaatName")]
        public DictaatDetails Dictaat { get; set; }

        public virtual ICollection<DictaatSessionUser> Participants { get; set; }

    }
}
