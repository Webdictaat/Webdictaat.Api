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
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="isOwner">Default is false</param>
        public bool AuthorizeResource(string dictaatName, bool isOwner = false)
        {
            var authorize = false;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (_authService.isAdmin(userId).Result)
                return true;

            authorize = _authService.isDictaatOwner(userId, dictaatName).Result;

            if (!authorize && !isOwner)
                authorize = _authService.IsDictaatContributer(userId, dictaatName).Result;

            if (!authorize)
                HttpContext.Response.StatusCode = 403;

            return authorize;
        }
    }
}
