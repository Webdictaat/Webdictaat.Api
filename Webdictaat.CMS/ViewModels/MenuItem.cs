using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.CMS.ViewModels
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
        }

        public string Name { get; set; }

        public string Url { get; set; }

        internal Domain.MenuItem ToPoco()
        {
            return new Domain.MenuItem()
            {
                Name = this.Name,
                Url = this.Url,
            }; 
        }
    }
}
