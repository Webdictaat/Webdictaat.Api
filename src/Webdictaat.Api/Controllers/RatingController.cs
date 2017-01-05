using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.CMS.Models;
using Webdictaat.CMS.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Webdictaat.CMS.Controllers
{

    /// <summary>
    /// Rating controller has all the routes for managing ratings
    /// </summary>
    [Route("api/dictaten/{dictaatName}/[controller]")]
    public class RatingController : Controller
    {
        private IRatingRepository _ratingRepo;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="ratingRepo"></param>
        public RatingController(IRatingRepository ratingRepo)
        {
            _ratingRepo = ratingRepo;
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
            RatingVM result = _ratingRepo.GetRating(ratingId);
            return result;
           
        }
      
        /// <summary>
        /// Create a new rating for a specific dictaat
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="rating">
        /// Title and Description are required
        /// </param>
        /// <returns></returns>
        [HttpPost] 
        public RatingVM Post(string dictaatName, [FromBody]RatingVM rating)
        {
            RatingVM result = _ratingRepo.CreateRating(rating);
            return result;
        }

    }
}
