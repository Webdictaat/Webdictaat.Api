using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.Domain;
using Webdictaat.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Webdictaat.Api.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webdictaat.Api.Controllers
{
    /// <summary>
    /// Authorized (Requires the user to be logged in.)
    /// </summary>
    [Route("api/dictaten/{dictaatName}/[controller]")]
    [Authorize]
    public class PagesController : Controller
    {
        private IPageRepository _pageRepo;
        private IMenuRepository _menuRepo;
        private IAuthorizeService _authorizeService;

        public PagesController(
            IPageRepository pageRepo, 
            IMenuRepository menuRepo,
            IAuthorizeService authorizeService)
        {
            _pageRepo = pageRepo;
            _menuRepo = menuRepo;
            _authorizeService = authorizeService;
        }

        [HttpGet("{pageName}")]
        public ViewModels.DictaatPage Get(string dictaatName, string pageName)
        {
            return _pageRepo.GetDictaatPage(dictaatName, pageName);
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<ViewModels.DictaatPageSummary> Get(string dictaatName)
        {
            return _pageRepo.GetDictaatPages(dictaatName);
        }

        // POST api/values
        [HttpPost]
        public List<ViewModels.MenuItem> Post(string dictaatName, [FromBody]ViewModels.DictaatPageForm form)
        {
            var MenuItem = new ViewModels.MenuItem()
            {
                Name = form.Page.Name,
                Url = form.Page.Url
            };
            
            var result = _pageRepo.CreateDictaatPage(dictaatName, form.Page, form.TemplateName); 
            var menu = _menuRepo.AddMenuItem(dictaatName, form.SubMenu, MenuItem);
            return menu;
        }


        // POST api/values
        [HttpPut("{pageName}")]
        public async Task<ViewModels.DictaatPage> Put(string dictaatName, string pageName, [FromBody]ViewModels.DictaatPage page)
        {
            if (!await _authorizeService.IsDictaatContributer(User.Identity.Name, dictaatName))
            {
                HttpContext.Response.StatusCode = 403;
                return null;
            }

            return _pageRepo.EditDictaatPage(dictaatName, page);
        }



        // POST api/values
        [HttpDelete("{pageName}")]
        public async Task<List<ViewModels.MenuItem>> Delete(string dictaatName, string pageName)
        {
            if (!await _authorizeService.IsDictaatContributer(User.Identity.Name, dictaatName))
            {
                HttpContext.Response.StatusCode = 403;
                return null;
            }

            var menu = _menuRepo.RemoveMenuItem(dictaatName, pageName);
            _pageRepo.DeleteDictaatPage(dictaatName, pageName);
            return menu;
        }

    }
}
