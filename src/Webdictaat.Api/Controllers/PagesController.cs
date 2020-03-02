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
    public class PagesController :BaseController
    {
        private IPageRepository _pageRepo;
        private IMenuRepository _menuRepo;
        private IAuthorizeService _authorizeService;

        
        public PagesController(
            IPageRepository pageRepo, 
            IMenuRepository menuRepo,
            IAuthorizeService authorizeService) : base(authorizeService)
        {
            _pageRepo = pageRepo;
            _menuRepo = menuRepo;
            _authorizeService = authorizeService;
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        [HttpGet("{pageName}")]
        public ViewModels.DictaatPage Get(string dictaatName, string pageName)
        {
            return _pageRepo.GetDictaatPage(dictaatName, pageName);
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ViewModels.DictaatPageSummary>> Get(string dictaatName)
        {
            if (!AuthorizeResource(dictaatName))    
                return null;

            return _pageRepo.GetDictaatPages(dictaatName);
        }

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ViewModels.MenuItem>> Post(string dictaatName, [FromBody]ViewModels.DictaatPageForm form)
        {
            if (!AuthorizeResource(dictaatName))
                return null;

            if(!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;
                return null;
            }

            var MenuItem = new ViewModels.MenuItem()
            {
                Name = form.Page.Name,
                Url = form.Page.Url
            };
            
            var result = _pageRepo.CreateDictaatPage(dictaatName, form.Page, form.TemplateName); 
            var menu = _menuRepo.AddMenuItem(dictaatName, form.SubMenu, MenuItem);
            return menu;
        }


        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="pageName"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpPut("{pageName}")]
        public async Task<ViewModels.DictaatPage> Put(string dictaatName, string pageName, [FromBody]ViewModels.DictaatPage page)
        {
            if (!AuthorizeResource(dictaatName))
                return null;

            return _pageRepo.EditDictaatPage(dictaatName, page);
        }



        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        [HttpDelete("{pageName}")]
        public async Task<List<ViewModels.MenuItem>> Delete(string dictaatName, string pageName)
        {
            if (!AuthorizeResource(dictaatName))
                return null;

            var menu = _menuRepo.RemoveMenuItem(dictaatName, pageName);
            _pageRepo.DeleteDictaatPage(dictaatName, pageName);
            return menu;
        }

    }
}
