using System.Collections.Generic;
using Webdictaat.Api.Models;
using Webdictaat.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Webdictaat.Domain.User;


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
    }
}
