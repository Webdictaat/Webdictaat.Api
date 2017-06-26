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
    }
}
