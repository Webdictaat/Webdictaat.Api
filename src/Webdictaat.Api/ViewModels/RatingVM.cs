using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Domain;

namespace Webdictaat.CMS.ViewModels
{
    public class RatingVM
    {
        public RatingVM()
        {

        }

        public RatingVM(Rating rating)
        {
            this.HappyCounter = rating.Rates.Count(r => r.Emotion == (int)Emotion.Happy);
            this.SadCounter = rating.Rates.Count(r => r.Emotion == (int)Emotion.Sad);
            this.Id = rating.Id;
            this.Title = rating.Title;
            this.Description = rating.Description;
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public RateVM MyRate { get; set; }
        public int HappyCounter { get; private set; }
        public int SadCounter { get; private set; }
    }
}
