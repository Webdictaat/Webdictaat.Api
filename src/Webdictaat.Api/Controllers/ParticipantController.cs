using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Webdictaat.Api.Services;
using Microsoft.AspNetCore.Identity;
using Webdictaat.Domain.User;
using Webdictaat.Data;
using Webdictaat.Api.ViewModels;
using Webdictaat.Api.ViewModels.Participant;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Webdictaat.Api.Controllers
{
    /// <summary>
    /// Acces point for participants and groups
    /// </summary>
    [Route("api/dictaten/{dictaatName}/")]
    public class ParticipantController : BaseController
    {
        private IParticipantRepository _participantRepository;
        private UserManager<ApplicationUser> _userManager;

        public ParticipantController(
            IParticipantRepository participantRepository,
            UserManager<Domain.User.ApplicationUser> userManager,
            IAuthorizeService authorizationService) : base(authorizationService)
        {
            _participantRepository = participantRepository;
            _userManager = userManager;
        }


        /// <summary>
        /// Returns a list of participants with points gained from this dictaat
        /// </summary>
        /// <returns></returns>
        [HttpGet("participants")]
        public IEnumerable<UserVM> GetParticipants(string dictaatName)
        {
            return _participantRepository.GetParticipants(dictaatName);
        }

        /// <summary>
        /// Returns a list of participants with points gained from this dictaat
        /// </summary>
        /// <returns></returns>
        [HttpGet("participants/{email}")]
        public ParticipantVM GetParticipant(string dictaatName, string email)
        {
            return _participantRepository.GetParticipant(dictaatName, email);
        }

        /// <summary>
        /// A route to join a dictaat by posting on it's participants list 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("groups/{group}")]
        public Boolean Join(string dictaatName, string group)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            return _participantRepository.Join(dictaatName, group, userId);
        }

        /// <summary>
        /// A route to join a dictaat by posting on it's participants list 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPut("groups/{group}")]
        public Boolean Switch(string dictaatName, string group)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            return _participantRepository.Join(dictaatName, group, userId);
        }


        /// <summary>
        /// A Route to get a list of groups
        /// </summary>
        /// <returns></returns>
        [HttpGet("groups")]
        public IEnumerable<GroupVM> GetGroups(string dictaatName)
        {
            return _participantRepository.GetGroups(dictaatName);
        }

        /// <summary>
        /// A route to join a dictaat by posting on it's participants list 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("groups")]
        public IEnumerable<GroupVM> Create(string dictaatName, [FromBody] IEnumerable<GroupVM> groups)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            return _participantRepository.CreateGroups(dictaatName, groups);
        }

        /// <summary>
        /// A route to join a dictaat by posting on it's participants list 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("groups/{group}")]
        public IEnumerable<GroupVM> Remove(string dictaatName, string group)
        {
            return _participantRepository.RemoveGroup(dictaatName, group);

        }
    }
}
