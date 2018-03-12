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
using Webdictaat.Api.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webdictaat.Api.Controllers
{
    /// <summary>
    /// CRUD Dictaat
    /// </summary>
    [Controller]
    [Route("api/[controller]")]
    public class DictatenController : BaseController
    {
        private IDictaatRepository _dictaatRepo;
        IAuthorizeService _authorizationService;
        private UserManager<ApplicationUser> _userManager;

        public DictatenController(
            IDictaatRepository dictaatRepo,
            UserManager<Domain.User.ApplicationUser> userManager,
            IAuthorizeService authorizationService) : base(authorizationService)
        {
            _authorizationService = authorizationService;
            _dictaatRepo = dictaatRepo;
            _userManager = userManager;
        }

        /// <summary>
        /// Create Dictaat
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IEnumerable<ViewModels.DictaatSummary>> Post([FromBody]ViewModels.DictaatForm form)
        {
            if (!ModelState.IsValid) {
                this.HttpContext.Response.StatusCode = 400; // I'm a teapot
                return null;
            }

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
        /// Get One
        /// Authorized (Requires the user to be logged in.)
        /// Returns a detailed summary of 1 webdictaat
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <returns></returns>
        [HttpGet("{dictaatName}")]
        [Authorize]
        public Task<ViewModels.Dictaat> Get(string dictaatName)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            return _dictaatRepo.getDictaat(dictaatName);
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
            var response = _dictaatRepo.getMarkings(name);
            return response;
        }


        /// <summary>
        /// Authorized (Requires the user to be logged in)
        /// 
        /// </summary>
        /// <param name="name">Name of the dictaat to be deleted</param>
        /// <returns>Returns success or fail (true of false)</returns>
        [HttpDelete("{dictaatName}")]
        [Authorize]
        public bool Delete(string dictaatName)
        {
            if (!AuthorizeResrouce(dictaatName, true))
                return false;

            //Nog niet goed nagedacht over wat er fout kan gaan bij het deleten.
            //Dus nu maar even op een vieze manier goed of fout checken
            try
            {
                _dictaatRepo.DeleteRepo(dictaatName);
                return true;
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
                return false;
            }
        }


        [Authorize]
        [HttpGet("{dictaatName}/contributers")]
        public IEnumerable<UserVM> GetContributers(string dictaatName)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            return _dictaatRepo.GetContributers(dictaatName);
        }

        /// <summary>
        /// Give a user editing rights for a given dictaat
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="contributer"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{dictaatName}/contributers")]
        public IEnumerable<UserVM> AddContributer(string dictaatName, [FromBody]UserVM contributer)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            var response = _dictaatRepo.AddContributer(dictaatName, contributer.Email);

            if (response == null)
                Response.StatusCode = 404;

            return response;
        }

        [HttpPost("{dictaatName}/copies")]
        public ViewModels.Dictaat CopyDictaat(string dictaatName, CopyDictaatForm form)
        {
            return _dictaatRepo.CopyDictaat(dictaatName, form);
        }
    }
}
