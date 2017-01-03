using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    public class Menu
    {
        public string Name { get; set; }

        public IEnumerable<Menu> SubMenus { get; set; }

        public IEnumerable<MenuItem> MenuItems { get; set; }

        public Menu()
        {
            SubMenus = new List<Menu>();
            MenuItems = new List<MenuItem>();
        }

    }
}
