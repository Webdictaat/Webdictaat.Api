using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.ViewModels
{
    public class UserAchievementVM
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int AchievementId { get; set; }

        public virtual Achievement Achievement { get; set; }

        public DateTime Timestamp { get; set; }

        public UserAchievementVM()
        {

        }

        public UserAchievementVM(string userid, ApplicationUser user, int achievementid, Achievement achievement, DateTime timestamp)
        {
            UserId = userid;
            User = user;
            AchievementId = achievementid;
            Achievement = achievement;
            Timestamp = timestamp;
        }

        public UserAchievementVM(UserAchievementVM userachievement)
        {
            UserId = userachievement.UserId;
            User = userachievement.User;
            AchievementId = userachievement.AchievementId;
            Achievement = userachievement.Achievement;
            Timestamp = userachievement.Timestamp;
        }


    }
}
