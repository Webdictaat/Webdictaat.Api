using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using Webdictaat.Core;
using Microsoft.Extensions.Options;
using Webdictaat.Domain;
using Microsoft.EntityFrameworkCore;
using Webdictaat.CMS.ViewModels;

namespace Webdictaat.CMS.Models
{
    public interface IRatingRepository
    {
        RatingVM CreateRating(RatingVM question);
        RatingVM GetRating(int ratingId);
    }

    /// <summary>
    /// Repositories turn domain models into view models.
    /// </summary>
    public class RatingRepository : IRatingRepository
    {
        private WebdictaatContext _context;

        public RatingRepository(WebdictaatContext context)
        {
            _context = context; 
        }

        /// <summary>
        /// Create a new rating based on a VM
        /// </summary>
        /// <param name="rating"></param>
        /// <returns></returns>
        public RatingVM CreateRating(RatingVM rating)
        {
            var r = new Rating()
            {
                Title = rating.Title,
                Description = rating.Description,
            };

            _context.Ratings.Add(r);
            _context.SaveChanges();
            rating.Id = r.Id;
            return rating;
                
        }

        /// <summary>
        /// returns null if no  question with that Id
        /// </summary>
        /// <param name="ratingId"></param>
        /// <returns></returns>
        public RatingVM GetRating(int ratingId)
        {
            Rating rating = _context.Ratings.FirstOrDefault(q => q.Id == ratingId);

            if (rating == null)
                return null;

            return new RatingVM(rating);
        }
    }
}