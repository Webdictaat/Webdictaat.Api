using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.ViewModels
{
    public class User
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public User(ApplicationUser user)
        {
            this.Username = user.UserName;
            this.Email = user.Email;
        }

   
    }
}
