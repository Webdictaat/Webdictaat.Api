using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webdictaat.Domain
{
    public class DictaatSession
    {
        [Key]
        public int Id { get; set; }

        public DateTime? StartedOn { get; set; }

        public DateTime? EndedOn { get; set; }

        [ForeignKey("DictaatDetailsId")]
        public virtual DictaatDetails DictaatDetails { get; set; }

        public string DictaatDetailsId { get; set; }

        public virtual ICollection<DictaatSessionUser> Participants { get; set; }

        public DictaatSession()
        {
            this.StartedOn = DateTime.Now;
        }
    }
}