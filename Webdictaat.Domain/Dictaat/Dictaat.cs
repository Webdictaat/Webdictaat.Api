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

        public IEnumerable<FileSummary> Pages { get; set; }

        public Menu Menu { get; set; }
        public string PagesDirectory { get; set; }

        public Dictaat()
        {
            Pages = new List<FileSummary>();
        }
    }
}
