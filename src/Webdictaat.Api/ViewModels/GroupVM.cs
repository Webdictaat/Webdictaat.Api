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
            this.TotalPoints = this.Members.Sum(m => m.Points);
        }

        public string GroupName { get; set; }

        public double TotalPoints { get; set; }

        public IEnumerable<UserVM> Members { get; set; }


    }
}
