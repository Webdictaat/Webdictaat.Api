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
    [Route("api/[controller]")]
    public class DictatenController : BaseController
    {
        private IDictaatRepository _dictaatRepo;
        IAuthorizeService _authorizationService;
        private UserManager<ApplicationUser> _userManager;

        public DictatenController(
            IDictaatRepository dictaatRepo,
            UserManager<Domain.User.ApplicationUser> userManager,
            IAuthorizeService authorizationService,
            WebdictaatContext context) : base(authorizationService)
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
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{dictaatName}")]
        [Authorize]
        public ViewModels.Dictaat Get(string dictaatName)
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
        [HttpPost("{dictaatName}/participants/{group}")]
        public Boolean Join(string dictaatName, string group)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            return _dictaatRepo.Join(dictaatName, group, userId);
        }

        /// <summary>
        /// Returns a list of participants with points gained from this dictaat
        /// </summary>
        /// <returns></returns>
        [HttpGet("{dictaatName}/participants")]
        public IEnumerable<UserVM> GetParticipants(string dictaatName)
        {
            return _dictaatRepo.GetParticipants(dictaatName);
        }

        [Authorize]
        [HttpGet("{dictaatName}/contributers")]
        public IEnumerable<UserVM> GetContributers(string dictaatName)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            return _dictaatRepo.GetContributers(dictaatName);
        }

        [Authorize]
        [HttpPost("{dictaatName}/contributers")]
        public IEnumerable<UserVM> AddContributer(string dictaatName, [FromBody]UserVM contributer)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            return _dictaatRepo.AddContributer(dictaatName, contributer.Email);
        }

        /// <summary>
        /// A Route to get a list of groups
        /// </summary>
        /// <returns></returns>
        [HttpGet("{dictaatName}/groups")]
        public IEnumerable<GroupVM> GetGroups(string dictaatName)  {
            return _dictaatRepo.GetGroups(dictaatName);
        }


}
}
