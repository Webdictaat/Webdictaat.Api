using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.CMS.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult LogIn()
        {
            return null;
        }

        [HttpGet]
        public ActionResult Callback()
        {
            return null;
        }
    }
}
