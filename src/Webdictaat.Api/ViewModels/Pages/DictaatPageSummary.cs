using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;
using Webdictaat.Domain.Google;

namespace Webdictaat.Api.ViewModels
{
    public class DictaatPageSummary
    {
        
        [RegularExpression("[^#]*", ErrorMessage = "Character is not allowed.")]
        public string Name { get; set; }

        [RegularExpression("[^#]*", ErrorMessage = "Character is not allowed.")]
        public string Url { get; set; }

        public DateTime LastChanged { get; set; }


        public DictaatPageSummary(){}

        public DictaatPageSummary(FileSummary fileSummary)
        {
            this.Name = fileSummary.Name;
            this.LastChanged = fileSummary.LastChanged;
        }




    }
}
