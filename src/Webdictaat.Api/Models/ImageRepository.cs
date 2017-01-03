using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Core;
using Webdictaat.Core.Helper;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Webdictaat.CMS.Models
{
    public interface IImageRepository
    {
        string CreateImage(string dictaatName, IFormFile file);
    }

    public class ImageRepository : IImageRepository
    {
        private PathHelper _pathHelper;

        public ImageRepository(
            IOptions<ConfigVariables> appSettings,
            Core.IDirectory directory,
            Core.IFile file)
        {
            _pathHelper = new PathHelper(appSettings.Value);
        }

        public string CreateImage(string dictaatName, IFormFile file) 
        {
            var path = _pathHelper.ImagesPath();

            var extension = System.IO.Path.GetExtension(file.FileName);
            var myUniqueFileName = string.Format(@"{0}{1}", Guid.NewGuid(), extension);

            using (var fileStream = new FileStream(Path.Combine(path, myUniqueFileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return myUniqueFileName;
        }
    }
}
