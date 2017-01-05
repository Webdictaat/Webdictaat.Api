using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using Webdictaat.Core;
using Microsoft.Extensions.Options;
using Webdictaat.Domain;
using Microsoft.EntityFrameworkCore;
using Webdictaat.CMS.ViewModels;
using Webdictaat.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Webdictaat.Domain.User;
using Microsoft.AspNet.Http;

namespace Webdictaat.CMS.Models
{
    public interface IRatingRepository
    {
        RatingVM CreateRating(RatingVM question);
        RatingVM GetRating(int ratingId, string userId);
        RateVM CreateRate(int ratingId, string userId, RateVM rate);
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

        public RateVM CreateRate(int ratingId, string userId, RateVM rate)
        {
            var r = new Rate()
            {
                Emotion = (int)rate.Emotion,
                Feedback = rate.Feedback,
                Timestamp = DateTime.Now,
                UserId = userId,
            };

            Rating rating = _context.Ratings
                .Include(ra => ra.Rates)
                .FirstOrDefault(q => q.Id == ratingId);

            rating.Rates.Add(r);
            _context.SaveChanges();
            rate.Id = r.Id;
            return rate;
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
        public RatingVM GetRating(int ratingId, string userId)
        {
            Rating rating = _context.Ratings.FirstOrDefault(q => q.Id == ratingId);

            if (rating == null)
                return null;

            var result = new RatingVM(rating);

            if (userId != null)
            {
                Rate rate = _context.Rates.FirstOrDefault(r => r.RatingId == rating.Id && r.UserId == userId);

                if(rate != null)
                    result.MyRate = new RateVM(rate);
            }

            return result;
        }
    }
}