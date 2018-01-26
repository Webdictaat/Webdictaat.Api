using System.ComponentModel.DataAnnotations.Schema;
using Webdictaat.Domain.User;

namespace Webdictaat.Domain
{
    public class DictaatSessionUser
    {
        [ForeignKey("DictaatSessionId")]
        public virtual DictaatSession DictaatSession { get; set; }

        public int DictaatSessionId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public string Group { get; set; }
        public string DictaatName { get; set; }

        [ForeignKey("Group, DictaatName")]
        public DictaatGroup DictaatGroup { get; set; }

        public string UserId { get; set; }
    }
}