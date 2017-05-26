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
        AchievementVM GetAchievement(int achievementId, string dictaatName);
        List<AchievementVM> GetAllAchievements(string dictaatName);
        void CheckAchievement(string dictaatName, int AchievementId, Boolean check, string userId);
        void AddAchievement(string dictaatName, Achievement achieve);
        void DeleteAchievement(string dictaatName, int achievementId);
        void UpdateAchievement(string dictaatName, int achievementId, Achievement achieve);

        List<AchievementGroupVM> GetAchievementGroups(string dictaatName);
        AchievementGroupVM GetAchievementGroup(string dictaatName, string groupName);
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

        public AchievementVM GetAchievement(int achievementId, string dictaatName)
        {
            Achievement achiev = _context.Achievements.FirstOrDefault(a => a.Id == achievementId);

            if (achiev == null)
            {
                return null;
            }

            AchievementVM achievVM = new AchievementVM(achiev);
            return achievVM;
        }

        public List<AchievementVM> GetAllAchievements(string dictaatName)
        {
            List<Achievement> achievlist = _context.Achievements.ToList();

            return achievlist.Select(q => new AchievementVM(q)).ToList();
        }

        public void UpdateAchievement(string dictaatName, int achievementId, Achievement achieve)
        {
            throw new NotImplementedException();
        }

        public List<AchievementGroupVM> GetAchievementGroups(string dictaatName)
        {
            List<DictaatAchievement> dictaatAchievements = _context.DictaatAchievements
                .Where(a => a.DictaatName == dictaatName)
                .ToList();

            List<AchievementGroupVM> result = new List<AchievementGroupVM>();
            return result;        
        }

        public AchievementGroupVM GetAchievementGroup(string dictaatName, string groupName)
        {
            List<DictaatAchievement> dictaatAchievements = _context.DictaatAchievements
                .Where(a => a.DictaatName == dictaatName && a.GroupName == groupName)
                .ToList();

            AchievementGroupVM result = new AchievementGroupVM();

            foreach (DictaatAchievement a in dictaatAchievements)
            {

            }

            
            return result;
        }
    }
}