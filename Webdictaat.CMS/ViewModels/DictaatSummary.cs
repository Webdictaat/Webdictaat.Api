using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat;
using Webdictaat.Core;

namespace Webdictaat.CMS.ViewModels
{
    public class DictaatSummary
    {
        public string Name { get; set; }

        public DateTime LastChange { get; set; }

        public DictaatSummary(Domain.DirectorySummary directorySummary)
        {
            this.LastChange = directorySummary.LastChange;
            this.Name = directorySummary.Name;
        }
    }
}
