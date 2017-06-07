using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Models;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.Controllers
{
    [Route("api/dictaten/{dictaatName}/sessions")]
    public class SessionController : Controller
    {
        private IDictaatRepository _dictaatRepo;
        private UserManager<ApplicationUser> _userManager;

        public SessionController(
            IDictaatRepository dictaatRepo,
            UserManager<Domain.User.ApplicationUser> userManager)
        {
            this._userManager = userManager;
            this._dictaatRepo = dictaatRepo;
        }

        [HttpGet("current")]
        public ViewModels.Session Get(string dictaatName)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            return this._dictaatRepo.GetCurrentSession(dictaatName, userId);
        }
    }
}
