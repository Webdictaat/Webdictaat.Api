using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Api.ViewModels
{
    public class AchievementGroupVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public List<AchievementVM> Achievememts { get; set; }

        public AchievementGroupVM()
        {

        }

        public AchievementGroupVM(int id, string name, int order, List<AchievementVM> achievements)
        {
            this.Id = id;
            this.Name = name;
            this.Order = order;
            this.Achievememts = achievements;
        }

        public AchievementGroupVM(AchievementGroupVM achievegroupvm)
        {
            this.Id = achievegroupvm.Id;
            this.Name = achievegroupvm.Name;
            this.Order = achievegroupvm.Order;
            this.Achievememts = achievegroupvm.Achievememts;
        }
    }
}
