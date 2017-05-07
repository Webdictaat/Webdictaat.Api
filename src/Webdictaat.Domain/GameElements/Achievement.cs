using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    public class Achievement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Boolean Hidden { get; set; }

        [Required]
        public Boolean Completed { get; set; }

        [Required]
        public string Image { get; set; }

    }
}
