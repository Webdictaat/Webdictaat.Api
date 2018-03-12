using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Api.ViewModels
{
    public class DictaatForm
    {
        /// <summary>
        /// Name of the dictaat
        /// </summary>
        [Required]
        [MaxLength(20)]
        [RegularExpression("^[a-zA-Z0-9_]*$")]
        public string Name { get; set; }

        public string Template { get; set; }
    }
}
