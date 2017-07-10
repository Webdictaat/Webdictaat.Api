using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.User;

namespace Webdictaat.Domain
{
    
    public class DictaatAchievement
    {

        public string DictaatName { get; set; }

        [ForeignKey("DictaatName")]

        public virtual DictaatDetails Dictaat { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Required]
        public int GroupOrder { get; set; }

        public int AchievementId { get; set; }

        [ForeignKey("AchievementId")]

        public virtual Achievement Achievement { get; set; }
    }

    public class UserAchievement
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public int AchievementId { get; set; }

        [Required]
        [ForeignKey("AchievementId")]
        public virtual Achievement Achievement { get; set; }

        public DateTime Timestamp { get; set; }
    }
    
    public class Achievement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Boolean Hidden { get; set; }

        [Required]
        public string Image { get; set; }

        [ForeignKey("DictaatName")]
        public string DictaatName { get; set; }
    }
}