using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            this.Id = rating.Id;
            this.Title = rating.Title;
            this.Description = rating.Description;
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
