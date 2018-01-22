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
    public class StylingController : BaseController
    {
        private IStyleRepository _styleRepo;
        private IAuthorizeService _authorizeService;

        
        public StylingController(
            IStyleRepository styleRepo, 
            IMenuRepository menuRepo,
            IAuthorizeService authorizeService) : base(authorizeService)
        {
            _styleRepo = styleRepo;
            _authorizeService = authorizeService;
        }

        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="styleName"></param>
        /// <returns></returns>
        [HttpGet("{styleName}")]
        public String Get(string dictaatName, string styleName)
        {
            return _styleRepo.GetDictaatStyling(dictaatName, styleName);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="styleName"></param>
        /// <param name="styleContent"></param>
        /// <returns></returns>
        [HttpPut("{styleName}")]
        public async Task<String> Put(string dictaatName, string styleName, [FromBody]ViewModels.StyleVM styleContent)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            return _styleRepo.EditDictaatStyling(dictaatName, styleName, styleContent.Content);
        }
    }
}
