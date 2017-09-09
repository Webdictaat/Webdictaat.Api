using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class MenuItem
    {

        public MenuItem()
        {

        }

        public MenuItem(Domain.MenuItem item)
        {
            this.Name = item.Name;
            this.Url = item.Url;
            this.IsEnabled = item.IsEnabled;
            this.MenuItems = new List<MenuItem>();

            if (item.MenuItems != null)
                this.MenuItems = item.MenuItems.Select(mi => new ViewModels.MenuItem(mi)).ToList();
            
        }

        public string Name { get; set; }

        public string Url { get; set; }
        public bool IsEnabled { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        internal Domain.MenuItem ToPoco()
        {
            return new Domain.MenuItem()
            {
                Name = this.Name,
                Url = this.Url,
                IsEnabled = this.IsEnabled,
                MenuItems = this.MenuItems != null ? this.MenuItems.Select(mi => mi.ToPoco()) : null
            }; 
        }
    }
}
