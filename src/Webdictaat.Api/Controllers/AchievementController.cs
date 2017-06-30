using System.Collections.Generic;
using Webdictaat.Api.Models;
using Webdictaat.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Webdictaat.Domain.User;
using System.Threading.Tasks;
using Webdictaat.Domain;
using System;

namespace Webdictaat.Api.Controllers
{
    [Route("api/dictaten/{dictaatName}/[controller]")]
    public class AchievementController : Controller
    {
        private IAchievementRepository _achievementRepo;
        private UserManager<ApplicationUser> _userManager;

        public AchievementController(IAchievementRepository achieveRepo, UserManager<ApplicationUser> userManager)
        {
            _achievementRepo = achieveRepo;
            _userManager = userManager;
        }

        [HttpGet]
        //[Authorize]
        public List<AchievementGroupVM> Get(string dictaatName)
        {
            List<AchievementGroupVM> result = _achievementRepo.GetAchievementGroups(dictaatName);
            return result;
        }

        [HttpGet("{userId}")]
        //[Authorize]
        public List<UserAchievementVM> GetUserAchievements(string userId, string dictaatName)
        {
            List<UserAchievementVM> result = _achievementRepo.GetUserAchievements(userId, dictaatName);
            return result;
        }

        [HttpPost("{achievementid}/user/{userid}")]
        public async Task<UserAchievementVM> PostUserAchievement(string userId, int achievementid)
        {
            UserAchievement userachiev = _achievementRepo.AddUserAchievement(achievementid, userId);
            UserAchievementVM result = new UserAchievementVM(userachiev);
            return result;
        }

        [HttpDelete("{achievementid}/user/{userid}")]
        public async Task<Boolean> DeleteUserAchievement(string userId, int achievementid)
        {
            return _achievementRepo.RemoveUserAchievement(achievementid, userId);
        }
    }
}
