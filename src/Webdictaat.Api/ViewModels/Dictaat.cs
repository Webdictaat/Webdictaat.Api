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

        public IEnumerable<DictaatPageSummary> Pages { get; set; }

        public List<ViewModels.MenuItem> MenuItems { get; set; }
        public UserVM Owner { get; }

        public Dictaat(Domain.Dictaat dictaat, DictaatDetails details, IList<Domain.Google.PageView> analytics)
        {
            this.Name = dictaat.Name;
            this.Pages = dictaat.Pages.Select(f => new DictaatPageSummary(f));
            this.MenuItems = dictaat.MenuItems.Select(mi => new ViewModels.MenuItem(mi, analytics)).ToList();
            this.Owner = new UserVM(details.DictaatOwner);
        }
    }
}
