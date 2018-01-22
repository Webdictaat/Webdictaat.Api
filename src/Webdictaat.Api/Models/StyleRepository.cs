using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Core;
using Webdictaat.Core.Helper;

namespace Webdictaat.Api.Models
{
    public interface IStyleRepository
    {
        String GetDictaatStyling(string dictaatName, string fileName = null);
        String EditDictaatStyling(string dictaatName, string fileName, string content);
    }

    public class StyleRepository : IStyleRepository
    {
        private Core.IFile _file;
        private Core.IDirectory _directory;


        private PathHelper _pathHelper;

        public StyleRepository(
            IOptions<ConfigVariables> appSettings, 
            Core.IDirectory directory,
            Core.IFile file)
        {
            _file = file;
            _directory = directory;
            _pathHelper = new PathHelper(appSettings.Value);
        }

        public String GetDictaatStyling(string dictaatName, string fileName = null)
        {
            string path = _pathHelper.StylePath(dictaatName, fileName);
            string content = _file.TryReadFile(path);

            if (content == null)
            {
                //wellicht in de toekomst 404 terug sturen? File not found?
                throw new System.IO.FileNotFoundException();
            }

            return content;
        }

        public String EditDictaatStyling(string dictaatName, string fileName, string content)
        {
            string path = _pathHelper.StylePath(dictaatName, fileName);

            if (!_file.TryEditFile(path, content))
            {
                //wellicht in de toekomst 404 terug sturen? File not found?
                throw new Exceptions.PageNotFoundException();
            }

            return GetDictaatStyling(dictaatName, fileName);
        }
    }
}
