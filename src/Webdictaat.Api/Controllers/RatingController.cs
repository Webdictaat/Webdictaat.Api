using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.Api.Models;
using Webdictaat.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Webdictaat.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Webdictaat.Domain.User;
using Webdictaat.Api.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webdictaat.Api.Controllers
{

    /// <summary>
    /// Rating controller has all the routes for managing ratings
    /// </summary>
    [Route("api/dictaten/{dictaatName}/[controller]")]
    public class RatingController :BaseController
    {
        private IRatingRepository _ratingRepo;
        private UserManager<ApplicationUser> _userManager;

        public RatingController(
            IRatingRepository ratingRepo,
            UserManager<ApplicationUser> userManager,
            IAuthorizeService authorizeService) : base(authorizeService)
        {
            _ratingRepo = ratingRepo;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets a rating based on id and given dictaat name
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="ratingId"></param>
        /// <returns>
        /// A rating object with title, description and Id
        /// </returns>
        [HttpGet("{ratingId}")]
        public RatingVM Get(string dictaatName, int ratingId)
        {

            string userId = _userManager.GetUserId(HttpContext.User);
            RatingVM result = _ratingRepo.GetRating(ratingId, userId);
            return result;
        }

        /// <summary>
        /// Create a new rating for a specific dictaat.
        /// Authorized (Requires the user to be logged in.)
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="rating">
        /// Title and Description are required
        /// </param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public RatingVM Post(string dictaatName, [FromBody]RatingVM rating)
        {
            if (!AuthorizeResource(dictaatName))
                return null;

            RatingVM result = _ratingRepo.CreateRating(dictaatName, rating);
            return result;
        }

        /// <summary>
        /// Authorized (Requires the user to be logged in.)
        /// </summary>
        /// <param name="ratingId"></param>
        /// <param name="rate"></param>
        /// <returns></returns>
        [HttpPost("{ratingId}/rates")]
        [Authorize]
        public RatingVM Post(int ratingId, [FromBody] RateVM rate)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            RateVM result = _ratingRepo.CreateRate(ratingId, userId, rate);
            return _ratingRepo.GetRating(ratingId, userId);         
        }

    }
}
