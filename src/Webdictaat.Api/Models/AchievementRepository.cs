using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Domain;

namespace Webdictaat.Api.Models
{
    public interface IAchievementRepository
    {
        AchievementVM GetAchievement(int achievementId, string dictaatName, string userId);
        AchievementVM GetAllAchievements(string dictaatName, string userId);
        void CheckAchievement(string dictaatName, int AchievementId, Boolean check, string userId);
        void AddAchievement(string dictaatName, Achievement achieve);
        void DeleteAchievement(string dictaatName, int achievementId);
        void UpdateAchievement(string dictaatName, int achievementId, Achievement achieve);
    }

    public class AchievementRepository : IAchievementRepository
    {
        public void AddAchievement(string dictaatName, Achievement achieve)
        {
            throw new NotImplementedException();
        }

        public void CheckAchievement(string dictaatName, int AchievementId, bool check, string userId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAchievement(string dictaatName, int achievementId)
        {
            throw new NotImplementedException();
        }

        public AchievementVM GetAchievement(int achievementId, string dictaatName, string userId)
        {
            throw new NotImplementedException();
        }

        public AchievementVM GetAllAchievements(string dictaatName, string userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateAchievement(string dictaatName, int achievementId, Achievement achieve)
        {
            throw new NotImplementedException();
        }
    }
}
