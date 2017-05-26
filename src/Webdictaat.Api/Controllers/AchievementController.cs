using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Webdictaat.Api.Services;
using Webdictaat.CMS.Models;
using Webdictaat.CMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.Api.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Webdictaat.Domain.User;
using Webdictaat.Domain;
using Webdictaat.Api.Models;

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

        [HttpGet("{achievementGroupName}")]
        //[Authorize]
        public AchievementGroupVM Get(string achievementGroupName, string dictaatName)
        {
            AchievementGroupVM result = _achievementRepo.GetAchievementGroup(dictaatName, achievementGroupName);
            return result;
        }
    }
}
