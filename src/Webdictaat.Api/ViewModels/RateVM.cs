using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Api.ViewModels
{
    public enum Emotion
    {
        Sad = 0, Happy = 1, 
    }

    public class RateVM
    {
        public RateVM()
        {

        }

        public RateVM(Domain.Rate rate)
        {
            this.Id = rate.Id;
            this.Emotion = (Emotion)rate.Emotion;
            this.Feedback = rate.Feedback;
        }

        public int Id { get; set; }

        [Required]
        public Emotion Emotion { get; set; }

        public string Feedback { get; set; }
    }
}
