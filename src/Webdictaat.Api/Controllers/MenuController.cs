using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Services;
using Webdictaat.Api.Models;

namespace Webdictaat.Api.Controllers
{
    /// <summary>
    /// Authorized (Requires the user to be logged in.)
    /// </summary>
    [Route("api/dictaten/{dictaatName}/[controller]")]
    [Authorize]
    public class MenuController :BaseController
    {
        private IMenuRepository _menuRepo;
        private IAuthorizeService _authorizeService;

        public MenuController(
            IMenuRepository menuRepo,
            IAuthorizeService authorizeService) : base(authorizeService)
        {
            _menuRepo = menuRepo;
            _authorizeService = authorizeService;
        }

        // GET: api/values
        [HttpGet]
        public List<ViewModels.MenuItem> Get(string dictaatName)
        {
            return _menuRepo.GetMenu(dictaatName);
        }

        // POST api/values
        [HttpPut]
        public List<ViewModels.MenuItem> Put(string dictaatName, [FromBody]List<ViewModels.MenuItem> menuItems)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            return _menuRepo.EditMenu(dictaatName, menuItems);
        }
    }
}
