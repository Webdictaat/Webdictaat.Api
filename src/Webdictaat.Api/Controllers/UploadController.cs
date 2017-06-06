using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Models;

namespace Webdictaat.Api.Controllers
{
    [Route("api/dictaten/{dictaatName}/[controller]")]
    public class UploadController
    {
        private IImageRepository _imageRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageRepo"></param>
        public UploadController(IImageRepository imageRepo)
        {
            this._imageRepo = imageRepo;
        }

        /// <summary>
        /// Authorized (Requires the user to be logged in.)
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public string Post(string dictaatName, IFormFile file)
        {

            if (file?.Length > 0)
            {
               return this._imageRepo.CreateImage(dictaatName, file);
            }

            return null;
        }
    }
}

