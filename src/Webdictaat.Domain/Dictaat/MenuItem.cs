using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    public class MenuItem
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public IEnumerable<MenuItem> MenuItems { get; set; }
        public bool IsDisabled { get; set; }
    }
}
