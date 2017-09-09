using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Models;
using Webdictaat.Api.Services;

namespace Webdictaat.Api.Controllers
{
    [Route("api/dictaten/{dictaatName}/[controller]")]
    public class UploadController :BaseController
    {
        private IImageRepository _imageRepo;


        public UploadController(
            IImageRepository imageRepo,
            IAuthorizeService authorizeService) : base(authorizeService)
        {
            this._imageRepo = imageRepo;
        }

        /// <summary>
        /// Create File
        /// Authorized (Requires the user to be logged in.)
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public string Post(string dictaatName, IFormFile file)
        {
            if (!AuthorizeResrouce(dictaatName))
                return null;

            if (file?.Length > 0)
            {
               return this._imageRepo.CreateImage(dictaatName, file);
            }

            return null;
        }
    }
}

