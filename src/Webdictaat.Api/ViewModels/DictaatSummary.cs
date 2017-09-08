using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat;
using Webdictaat.Api.ViewModels;
using Webdictaat.Core;
using Webdictaat.Domain;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.ViewModels
{
    public class DictaatSummary
    {

        public string Name { get; set; }
        public DateTime LastChange { get; set; }
        public String Owner { get; private set; }
        public ICollection<String> Contributers { get; set; }
        public bool IsEnabled { get; }
        public bool CanEdit { get; set; }

        public DictaatSummary(Domain.DirectorySummary directorySummary)
        {
            this.LastChange = directorySummary.LastChange;
            this.Name = directorySummary.Name;
        }

        public DictaatSummary(DictaatDetails dd, string userId = null)
        {
            this.LastChange = DateTime.Now;
            this.Name = dd.Name;
            this.Owner = dd.DictaatOwner.UserName;
            this.Contributers = dd.Contributers.Select(c => c.User.UserName).ToList();
            this.IsEnabled = dd.IsEnabled;

            if (userId != null)
            {
                this.CanEdit = dd.GetContributersIds().Contains(userId);
            }
            
        }
    }
}
