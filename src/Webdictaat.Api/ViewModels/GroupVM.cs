using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Api.ViewModels
{
    public class GroupVM
    {

        public GroupVM(string key, List<UserVM> list)
        {
            this.GroupName = key;
            this.Members = list;
            this.TotalPoints = (int) this.Members.Sum(m => m.Points);
            this.Points = this.TotalPoints / this.Members.Count();
        }

        public string GroupName { get; set; }

        public int TotalPoints { get; set; }

        public int Points { get; set; }

        public IEnumerable<UserVM> Members { get; set; }


    }
}
