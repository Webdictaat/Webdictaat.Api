using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class AchievementVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Boolean Hidden { get; set; }

        public Boolean Completed { get; set; }

        public string Image { get; set; }

        public AchievementVM(int id, string name, Boolean hidden, Boolean completed, string image)
        {
            this.Id = id;
            this.Name = name;
            this.Hidden = hidden;
            this.Completed = completed;
            this.Image = image;
        }

        public AchievementVM(Achievement achiev)
        {
            this.Id = achiev.Id;
            this.Name = achiev.Name;
            this.Hidden = achiev.Hidden;
            this.Completed = achiev.Completed;
            this.Image = achiev.Image;
        }
    }
}
