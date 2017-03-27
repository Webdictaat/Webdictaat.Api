using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.CMS.ViewModels
{
    public class Menu
    {
        public string Name { get; set; }

        public IList<Menu> SubMenus { get; set; }

        public IList<MenuItem> MenuItems { get; set; }


        public Menu(Domain.Menu menu)
        {
            this.Name = menu.Name;
            this.MenuItems = menu.MenuItems.Select(i => new ViewModels.MenuItem(i)).ToList();

        }

        public Domain.Menu ToPoco()
        {
            var menu = new Domain.Menu();
            menu.Name = this.Name;
            menu.MenuItems = this.MenuItems.Select(i => i.ToPoco()).ToList();
            return menu;
        }
    }
}
