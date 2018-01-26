using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class GroupVM
    {
        private IEnumerable<UserVM> participants;

        public GroupVM()
        {

        }

        public GroupVM(string key, List<UserVM> list)
        {
            this.GroupName = key;
            this.Members = list;

            if(Members.Count() != 0)
            {
                this.TotalPoints = (int)this.Members.Sum(m => m.Points);
                this.Points = this.TotalPoints / this.Members.Count();
            }
         
        }

        public string GroupName { get; set; }

        public int TotalPoints { get; set; }

        public int Points { get; set; }

        public IEnumerable<UserVM> Members { get; set; }


    }
}
