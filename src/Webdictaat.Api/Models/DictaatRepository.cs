using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using Webdictaat.Core;
using Microsoft.Extensions.Options;
using Webdictaat.CMS.ViewModels;
using Webdictaat.Core.Helper;

namespace Webdictaat.CMS.Models
{
    public interface IDictaatRepository
    {
        IEnumerable<ViewModels.DictaatSummary> GetDictaten();
        ViewModels.Dictaat getDictaat(string name);
        void CreateDictaat(string name, string template);
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

        private PathHelper _pathHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="directory"></param>
        /// <param name="dictaatFactory"></param>
        public DictaatRepository(
            IOptions<ConfigVariables> appSettings, 
            IDirectory directory,
             IFile file)
        {
            _directoryRoot = appSettings.Value.DictaatRoot;
            _pagesDirectory = appSettings.Value.PagesDirectory;
            _dictatenDirectory = appSettings.Value.DictatenDirectory;
            _templatesDirectory = appSettings.Value.TemplatesDirectory;
            var menuConfigName = appSettings.Value.MenuConfigName;
            _directory = directory;

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

        public void CreateDictaat(string name, string template)
        {
            string pagesPath = name + _pagesDirectory;
            Domain.Dictaat dictaat = _dictaatFactory.CreateDictaat(name, template);
        }

        public void deleteRepo(string name)
        {
            string dictaatPath = _pathHelper.DictaatPath(name);
            _dictaatFactory.DeleteDictaaat(dictaatPath);
        }
    }
}