using Microsoft.EntityFrameworkCore;
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

        public List<AchievementGroupVM> GetAchievementGroups(string dictaatName)
        {
            /*
            var dictaat =_context.DictaatDetails
                .Include("Achievements.Achievement")
                .FirstOrDefault(d => d.Name == dictaatName);

            var groups = dictaat.Achievements.GroupBy(a => a.GroupName);
            */

            List<string> groupNames = _context.DictaatAchievements
                .Where(x => x.DictaatName == dictaatName)
                .Select(x => x.GroupName).Distinct()
                .ToList();

            if(groupNames.Count == 0)
            {
                return null;
            }

            List<AchievementGroupVM> result = new List<AchievementGroupVM>();

            foreach (string a in groupNames)
            {
                result.Add(GetAchievementGroup(dictaatName, a));
            }
            
            return result;        
        }

        public AchievementGroupVM GetAchievementGroup(string dictaatName, string groupName)
        {
            List<DictaatAchievement> dictaatAchievements = _context.DictaatAchievements
                .Where(a => a.DictaatName == dictaatName && a.GroupName == groupName)
                .ToList();

            if(dictaatAchievements.Count == 0)
            {
                return null;
            }

            List<AchievementVM> achievements = new List<AchievementVM>();

            foreach (DictaatAchievement a in dictaatAchievements)
            {

                Achievement achiev = _context.Achievements.FirstOrDefault(b => b.Id == a.AchievementId);
                AchievementVM achievement = new AchievementVM(achiev);
                achievements.Add(achievement);
            }

            AchievementGroupVM result = new AchievementGroupVM(dictaatName, groupName, dictaatAchievements[0].GroupOrder, achievements);

            return result;
        }
    }
}