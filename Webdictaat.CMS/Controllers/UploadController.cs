using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.CMS.Models;

namespace Webdictaat.CMS.Controllers
{
    [Route("api/dictaten/{dictaatName}/[controller]")]
    public class UploadController
    {
        private IImageRepository _imageRepo;
        public UploadController(IImageRepository imageRepo)
        {
            this._imageRepo = imageRepo;
        }

        [HttpPost]
        public void Post(string dictaatName, ICollection<IFormFile> files)
        {
            List<string> result = new List<String>();

            foreach (var file in files)
            {
                
                if (file.Length > 0)
                {
                    result.Add(this._imageRepo.CreateImage(dictaatName, file));
                }
            }
            
        }
    }
}

