using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Webdictaat.Domain;
using Webdictaat.Data;
using Webdictaat.Api.Services;
using Webdictaat.Domain.User;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webdictaat.Api.Controllers
{
    [Route("api/[controller]")]
    public class DictatenController : Controller
    {
        private IDictaatRepository _dictaatRepo;
        IAuthorizeService _authorizationService;
        private UserManager<ApplicationUser> _userManager;

        public DictatenController(
            IDictaatRepository dictaatRepo,
            UserManager<Domain.User.ApplicationUser> userManager,
            IAuthorizeService authorizationService,
            WebdictaatContext context)
        {
            _authorizationService = authorizationService;
            _dictaatRepo = dictaatRepo;
            _userManager = userManager;
        }

        /// <summary>
        /// Returns a list of small summaries of webdictaten 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IEnumerable<ViewModels.DictaatSummary>> Post([FromBody]ViewModels.DictaatForm form)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _dictaatRepo.CreateDictaat(form.Name, user, form.Template);
            return this.Get();
        }

        /// <summary>
        /// Returns a list of small summaries of webdictaten 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<ViewModels.DictaatSummary> Get()
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            var dictaten = _dictaatRepo.GetDictaten(userId);
            return dictaten;
        }

        /// <summary>
        /// Authorized (Requires the user to be logged in.)
        /// Returns a detailed summary of 1 webdictaat
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [Authorize]
        public ViewModels.Dictaat Get(string name)
        {
            return _dictaatRepo.getDictaat(name);
        }

        /// <summary>
        /// Authorized (Requires the user to be logged in.)
        /// Returns a detailed summary of 1 webdictaat
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}/markings")]
        [Authorize]
        public ViewModels.DictaatMarkings GetMarkings(string name)
        {
            return _dictaatRepo.getMarkings(name);
        }


        /// <summary>
        /// Authorized (Requires the user to be logged in)
        /// 
        /// </summary>
        /// <param name="name">Name of the dictaat to be deleted</param>
        /// <returns>Returns success or fail (true of false)</returns>
        [HttpDelete("{name}")]
        [Authorize]
        public async Task<bool> Delete(string name)
        {
            if (!await _authorizationService.IsDictaatContributer(User.Identity.Name, name))
            {
                HttpContext.Response.StatusCode = 403;
                return false;
            }

            //Nog niet goed nagedacht over wat er fout kan gaan bij het deleten.
            //Dus nu maar even op een vieze manier goed of fout checken
            try
            {
                _dictaatRepo.DeleteRepo(name);
                return true;
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = 500;
                return false;
            }
        }

        /// <summary>
        /// A route to join a dictaat by posting on it's participants list 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{dictaatName}/participants")]
        public Boolean Join(string dictaatName )
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            return _dictaatRepo.Join(dictaatName, userId);
        }

    }
}
