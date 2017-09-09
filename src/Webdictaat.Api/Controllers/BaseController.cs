using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Models;
using Webdictaat.Api.Services;

namespace Webdictaat.Api
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
        public BaseController(IAuthorizeService authService) { 
            _authService = authService;
        }

        /// <summary>
        /// Throws a 403 if denied
        /// Returns if the user can access the resource
        /// </summary>
        /// <param name="dictaatName"></param>
        protected bool AuthorizeResrouce(string dictaatName)
        {
            if (!_authService.IsDictaatContributer(User.Identity.Name, dictaatName).Result)
            {
                HttpContext.Response.StatusCode = 403;
                return false;
            }

            return true;    
        }
    }
}
