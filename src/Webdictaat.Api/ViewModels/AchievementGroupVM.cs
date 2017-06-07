using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class AchievementGroupVM
    {
        public string DictaatId { get; set; }

        public string GroupName { get; set; }

        public int Order { get; set; }

        public List<AchievementVM> Achievements { get; set; }

        public AchievementGroupVM()
        {

        }

        public AchievementGroupVM(string id, string name, int order, List<AchievementVM> achievements)
        {
            this.DictaatId = id;
            this.GroupName = name;
            this.Order = order;
            this.Achievements = achievements;
        }

        public AchievementGroupVM(AchievementGroupVM achievegroupvm)
        {
            this.DictaatId = achievegroupvm.DictaatId;
            this.GroupName = achievegroupvm.GroupName;
            this.Order = achievegroupvm.Order;
            this.Achievements = achievegroupvm.Achievements;
        }
    }
}
