using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain
{
    public class Rate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Emotion { get; set; }

        [Required]
        public string Feedback { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        public string UserId { get; set; }

        [Required]
        public int RatingId { get; set; }

        [Required]
        public virtual Rating Rating { get; set; }


        public DateTime Timestamp { get; set; }
    }
}
