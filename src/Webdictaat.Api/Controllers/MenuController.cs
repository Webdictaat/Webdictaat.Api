using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Services;
using Webdictaat.CMS.Models;

namespace Webdictaat.Api.Controllers
{
    /// <summary>
    /// Authorized (Requires the user to be logged in.)
    /// </summary>
    [Route("api/dictaten/{dictaatName}/[controller]")]
    [Authorize]
    public class MenuController : Controller
    {
        private IMenuRepository _menuRepo;
        private IAuthorizeService _authorizeService;

        public MenuController(
            IMenuRepository menuRepo,
            IAuthorizeService authorizeService)
        {
            _menuRepo = menuRepo;
            _authorizeService = authorizeService;
        }

        // GET: api/values
        [HttpGet]
        public List<CMS.ViewModels.MenuItem> Get(string dictaatName)
        {
            return _menuRepo.GetMenu(dictaatName);
        }

        // POST api/values
        [HttpPut]
        public List<CMS.ViewModels.MenuItem> Put(string dictaatName, [FromBody]List<CMS.ViewModels.MenuItem> menuItems)
        {
            return _menuRepo.EditMenu(dictaatName, menuItems);
        }
    }
}
