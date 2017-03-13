using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using Webdictaat.Core;
using Microsoft.Extensions.Options;
using Webdictaat.Core.Helper;
using Webdictaat.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Webdictaat.Domain.User;
using Webdictaat.Data;

namespace Webdictaat.CMS.Models
{
    public interface IDictaatRepository
    {
        IEnumerable<ViewModels.DictaatSummary> GetDictaten();
        ViewModels.Dictaat getDictaat(string name);
        void CreateDictaat(string name, ApplicationUser user, string template);
        void deleteRepo(string name);
    }

    public class DictaatRepository : IDictaatRepository
    {
        private string _directoryRoot;
        private string _pagesDirectory;
        private string _templatesDirectory;
        private string _dictatenDirectory;

        private IDirectory _directory;
        private IDictaatFactory _dictaatFactory;

        private WebdictaatContext _context;


        private PathHelper _pathHelper;
        private UserManager<object> _userManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="directory"></param>
        /// <param name="dictaatFactory"></param>
        /// <param name="context"></param>
        public DictaatRepository(
            IOptions<ConfigVariables> appSettings, 
            IDirectory directory,
            IFile file,
            WebdictaatContext context)
        {
            _directoryRoot = appSettings.Value.DictaatRoot;
            _pagesDirectory = appSettings.Value.PagesDirectory;
            _dictatenDirectory = appSettings.Value.DictatenDirectory;
            _templatesDirectory = appSettings.Value.TemplatesDirectory;
            var menuConfigName = appSettings.Value.MenuConfigName;
            _directory = directory;
            _context = context;

            //best place to build the factory
            _dictaatFactory = new DictaatFactory(appSettings.Value, directory, file);
            _pathHelper = new PathHelper(appSettings.Value);

        }

        public IEnumerable<ViewModels.DictaatSummary> GetDictaten()
        {
            return _dictaatFactory.GetDictaten() 
                .Select(s => new ViewModels.DictaatSummary(s));
        }

        public ViewModels.Dictaat getDictaat(string name)
        {
            string pagesPath = name + _pagesDirectory;
            Domain.Dictaat dictaat = _dictaatFactory.GetDictaat(name);
            return new ViewModels.Dictaat(dictaat);
                
        }

        /// <summary>
        /// Create a new dictaat based on a given template. 
        /// </summary>
        /// <param name="name">Name of the dictaat. Must be unique.</param>
        /// <param name="template">optional template name, default = defaultTemplate</param>
        public void CreateDictaat(string name, ApplicationUser user, string template)
        {
            //Create the database entry
            var dictaatDetails = new DictaatDetails()
            {
                Name = name,
                DictaatOwnerId = user.Id,
            };

            _context.DictaatDetails.Add(dictaatDetails);
            _context.SaveChanges();

            //Create the folder
            Domain.Dictaat dictaat = _dictaatFactory.CreateDictaat(name, template);
        }

        /// <summary>
        /// Delete a dictaat with given name. 
        /// </summary>
        /// <param name="name"></param>
        public void deleteRepo(string name)
        {
            string dictaatPath = _pathHelper.DictaatPath(name);
            _dictaatFactory.DeleteDictaaat(dictaatPath);
        }
    }
}