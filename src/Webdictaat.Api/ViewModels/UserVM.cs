using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.ViewModels
{
    public class UserVM
    {
       

        public UserVM(ApplicationUser p)
        {
            this.Id = p.Id;
            this.Email = p.Email;
            this.UserName = p.UserName;
            this.Points = p.Points;
        }

        public string Email { get; set; }
        public string Id { get; private set; }
        public string UserName { get; set; }

        public double Points { get; set; }
    }
}
