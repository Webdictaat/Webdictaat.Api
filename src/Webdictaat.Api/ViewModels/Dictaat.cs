using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Models;
using Webdictaat.Core;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class Dictaat
    {
        public string Name { get; set; }

        public IEnumerable<FileSummary> Pages { get; set; }

        public List<ViewModels.MenuItem> MenuItems { get; set; }

        public Dictaat(Domain.Dictaat dictaat)
        {
            this.Name = dictaat.Name;
            this.Pages = dictaat.Pages;
            this.MenuItems = dictaat.MenuItems.Select(mi => new ViewModels.MenuItem(mi)).ToList();
        }
    }
}
