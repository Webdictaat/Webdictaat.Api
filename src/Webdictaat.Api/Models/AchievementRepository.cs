using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Data;
using Webdictaat.Domain;

namespace Webdictaat.Api.Models
{
    public interface IAchievementRepository
    {
        AchievementVM GetAchievement(int achievementId, string dictaatName, string userId);
        List<AchievementVM> GetAllAchievements(string dictaatName, string userId);
        void CheckAchievement(string dictaatName, int AchievementId, Boolean check, string userId);
        void AddAchievement(string dictaatName, Achievement achieve);
        void DeleteAchievement(string dictaatName, int achievementId);
        void UpdateAchievement(string dictaatName, int achievementId, Achievement achieve);
    }

    public class AchievementRepository : IAchievementRepository
    {
        private WebdictaatContext _context;

        public AchievementRepository(WebdictaatContext context)
        {
            _context = context;
        }

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
            Achievement achiev = _context.Achievements.First(a => a.Id == achievementId);
            
            if(achiev == null)
            {
                return null;
            }

            AchievementVM achievVM = new AchievementVM(achiev);
            return achievVM;
        }

        public List<AchievementVM> GetAllAchievements(string dictaatName, string userId)
        {
            List<AchievementVM> achievlist;


        }

        public void UpdateAchievement(string dictaatName, int achievementId, Achievement achieve)
        {
            throw new NotImplementedException();
        }
    }
}
