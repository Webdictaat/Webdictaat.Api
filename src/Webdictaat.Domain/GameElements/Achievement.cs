using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    public class DictaatAchievement
    {
        [ForeignKey("DictaatId")]
        public int DictaatId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Order { get; set; }

        [ForeignKey("AchievementId")]
        public int AchievementId { get; set; }
    }
    public class Achievement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Boolean Hidden { get; set; }
    }
}