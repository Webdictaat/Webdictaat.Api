using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Webdictaat.Api.Models;
using Webdictaat.Api.Services;

namespace Webdictaat.Api.Controllers
{
    /// <summary>
    /// Acts like a resource checker 
    /// </summary>
    public class BaseController : Controller
    {
        protected IAuthorizeService _authService;

        /// <summary>
        /// Returns the current user UserId (if any).
        /// </summary>
        protected string userId
        {
            get
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var idClaim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                return idClaim == null ? null : idClaim.Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authService"></param>
        public BaseController(IAuthorizeService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Throws a 403 if denied
        /// Returns if the user can access the resource
        /// This is a public method for testing purposes. 
        /// That's why it needs to be marked with NonAction.
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="isOwner">Default is false</param>
        [NonAction]
        public bool AuthorizeResource(string dictaatName, bool isOwner = false)
        {
            var authorize = false;

            if (_authService.isAdmin(this.userId).Result)
                return true;

            authorize = _authService.isDictaatOwner(this.userId, dictaatName).Result;

            if (!authorize && !isOwner)
                authorize = _authService.IsDictaatContributer(this.userId, dictaatName).Result;

            if (!authorize)
                HttpContext.Response.StatusCode = 403;

            return authorize;
        }


    }
}
