using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;
using Webdictaat.Domain.Google;

namespace Webdictaat.Api.ViewModels
{
    public class MenuItem
    {
        /// <summary>
        /// emtpy constructor required for mapping in ASP
        /// </summary>
        public MenuItem(){}

        public MenuItem(Domain.MenuItem item, IList<PageView> analytics = null)
        {
            this.Name = item.Name;
            this.Url = item.Url;
            this.IsDisabled = item.IsDisabled;
            this.MenuItems = new List<MenuItem>();

            if (item.MenuItems != null)
                this.MenuItems = item.MenuItems.Select(mi => new ViewModels.MenuItem(mi, analytics)).ToList();

            if (analytics != null && analytics.Any(a => a.PageUri == this.Url))
                this.PageViews = analytics.FirstOrDefault(a => a.PageUri == this.Url);

        }

        public PageView PageViews { get; set; }


        public string Name { get; set; }

        public string Url { get; set; }
        public bool IsDisabled { get; set; }
        public List<MenuItem> MenuItems { get; set; }

        internal Domain.MenuItem ToPoco()
        {
            return new Domain.MenuItem()
            {
                Name = this.Name,
                Url = this.Url,
                IsDisabled = this.IsDisabled,
                MenuItems = this.MenuItems != null ? this.MenuItems.Select(mi => mi.ToPoco()) : null
            }; 
        }
    }
}
