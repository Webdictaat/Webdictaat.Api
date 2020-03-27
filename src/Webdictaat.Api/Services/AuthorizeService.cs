using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Data;
using Webdictaat.Domain;

namespace Webdictaat.Api.Services
{
    /// <summary>
    /// Webdictaat service for authorization
    /// </summary>
    public interface IAuthorizeService
    {
        Task<bool> IsDictaatContributer(string userId, string dictaatName);
        Task<bool> isDictaatOwner(string userId, string dictaatName);
        Task<bool> isAdmin(string userId);
    }

    public class AuthorizeService : IAuthorizeService
    {
        private UserManager<Domain.User.ApplicationUser> _userManager;
        private WebdictaatContext _context;

        public AuthorizeService(
          UserManager<Domain.User.ApplicationUser> userManager,
          WebdictaatContext context)
        {
            this._context = context;
            this._userManager = userManager;
        }

        /// <summary>
        /// returns true of false, based on the role of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> isAdmin(string userId)
        {
            if (userId == null) return false;

            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.IsInRoleAsync(user, "Admin");
        }

        /// <summary>
        /// Returns true or false, based on the contributers of the resource
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="dictaatName"></param>
        /// <returns></returns>
        public async Task<bool> IsDictaatContributer(string userId, string dictaatName)
        {
            IResource details = _context.DictaatDetails.Include(dd => dd.Contributers).FirstOrDefault(dd => dd.Name == dictaatName);
            var user = await _userManager.FindByIdAsync(userId);
            return details.GetContributersIds().Contains(user.Id);
        }

        public async Task<bool> isDictaatOwner(string userId, string dictaatName)
        {
            DictaatDetails details = _context.DictaatDetails.FirstOrDefault(dd => dd.Name == dictaatName);
            var user = await _userManager.FindByIdAsync(userId);
            return details.DictaatOwnerId == user.Id;
        }
    }
}
