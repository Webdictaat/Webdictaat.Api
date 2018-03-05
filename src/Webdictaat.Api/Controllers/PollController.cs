using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Models;
using Webdictaat.Api.Services;
using Webdictaat.Api.ViewModels;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.Controllers
{
    /// <summary>
    /// Controller for retrieving and updating polls
    /// </summary>
    [Route("api/dictaten/{dictaatName}/[controller]")]
    public class PollController : BaseController
    {
        private IPollRepository _pollRepository;
        private IAuthorizeService _authorizeService;
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="pollRepository"></param>
        /// <param name="userManager"></param>
        /// <param name="authorizeService"></param>
        public PollController(
            IPollRepository pollRepository,
            UserManager<ApplicationUser> userManager,
            IAuthorizeService authorizeService) : base(authorizeService)
        {
            _pollRepository = pollRepository;
            _authorizeService = authorizeService;
            _userManager = userManager;
        }


        /// <summary>
        /// Get details of 1 poll
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="pollId"></param>
        /// <returns></returns>
        [HttpGet("{pollId}")]
        public PollVM Get(string dictaatName, int pollId)
        {
            var user = _userManager.GetUserId(HttpContext.User);

            return _pollRepository.GetPoll(pollId, user);
        }


        /// <summary>
        /// Create a new poll
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <returns></returns>
        [HttpPost()]
        [Authorize]
        public PollVM Create(string dictaatName, [FromBody] PollVM poll)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            return _pollRepository.CreatePoll(dictaatName, poll);
        }

        /// <summary>
        /// Update excisting poll
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="pollId"></param>
        /// <param name="poll"></param>
        /// <returns></returns>
        [HttpPut("{pollId}")]
        [Authorize]
        public PollVM Update(string dictaatName, int pollId, [FromBody] PollVM poll)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            return _pollRepository.UpdatePoll(dictaatName, pollId, poll);
        }

        /// <summary>
        /// Get all polls of 1 dictaat
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<PollVM> GetAll(string dictaatName)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            return _pollRepository.GetPolls(dictaatName);
        }

        /// <summary>
        /// Post a new vote
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="optionId"></param>
        /// <param name="pollId"></param>
        /// <returns></returns>
        [HttpPost("{pollId}/options/{optionId}/votes")]
        [Authorize]
        public PollVM Vote(string dictaatName, int optionId, int pollId)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            return _pollRepository.Vote(dictaatName, pollId, optionId, userId);
        }
    }
}
