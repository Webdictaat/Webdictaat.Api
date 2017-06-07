using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using Webdictaat.Core;
using Microsoft.Extensions.Options;
using Webdictaat.Domain;
using Microsoft.EntityFrameworkCore;
using Webdictaat.Api.ViewModels;
using Webdictaat.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Webdictaat.Domain.User;
using Microsoft.AspNet.Http;
using Webdictaat.Data;

namespace Webdictaat.Api.Models
{
    public interface IRatingRepository
    {
        RatingVM CreateRating(string dictaatName, RatingVM question);
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
        public RatingVM CreateRating(string dictaatName, RatingVM rating)
        {
            var r = new Rating()
            {
                DictaatDetailsName = dictaatName,
                Timestamp = DateTime.Now,
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
            Rating rating = _context.Ratings.Include("Rates").FirstOrDefault(q => q.Id == ratingId);

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