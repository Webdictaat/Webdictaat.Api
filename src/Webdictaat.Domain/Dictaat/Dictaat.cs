using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    public class Dictaat
    {
        public string Name { get; set; }

        public string Location { get; set; }

        public string Owner { get; set;  }

        public IEnumerable<FileSummary> Pages { get; set; }

        public IEnumerable<MenuItem> MenuItems { get; set; }

        public Dictaat()
        {
            Pages = new List<FileSummary>();
            MenuItems = new List<MenuItem>();
        }
    }
}
