using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.CMS.ViewModels
{
    public class DictaatPageSummary
    {
        public DictaatPageSummary()
        {

        }

        public DictaatPageSummary(FileSummary fileSummary)
        {
            this.Name = fileSummary.Name;
            this.LastChanged = fileSummary.LastChanged;
        }

        public string Name { get; set; }

        public DateTime LastChanged { get; set; }

    }
}
