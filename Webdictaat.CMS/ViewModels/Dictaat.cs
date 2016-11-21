using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.CMS.Models;
using Webdictaat.Core;
using Webdictaat.Domain;

namespace Webdictaat.CMS.ViewModels
{
    public class Dictaat
    {
        public string Name { get; set; }

        public IEnumerable<FileSummary> Pages { get; set; }

        public object Menu { get; set; }

        public Dictaat(Domain.Dictaat dictaat)
        {
            this.Name = dictaat.Name;
            this.Pages = dictaat.Pages;
            this.Menu = dictaat.Menu;
        }
    }
}
