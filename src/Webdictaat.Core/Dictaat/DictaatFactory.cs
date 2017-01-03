using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Core.Extensions;
using Webdictaat.Core.Helper;
using Webdictaat.Domain;


namespace Webdictaat.Core
{
    public interface IDictaatFactory
    {
        Domain.Dictaat GetDictaat(string name);
        Dictaat CreateDictaat(string name, string template);
        IEnumerable<DirectorySummary> GetDictaten();
    }

    public class DictaatFactory : IDictaatFactory
    {
        private PathHelper _pathHelper;

        private Core.IDirectory _directory;
        private Core.IFile _file;
        private Core.IMenuFactory _menuFactory;

        public DictaatFactory(ConfigVariables configVariables, Core.IDirectory directory, Core.IFile file)
        {
            _directory = directory;
            _file = file;
            _pathHelper = new PathHelper(configVariables);
            _menuFactory = new MenuFactory(configVariables, _file);
        }

        public Dictaat GetDictaat(string name)
        {
            Dictaat dictaat = new Dictaat();
            dictaat.Name = name;
            dictaat.Location = _pathHelper.DictaatPath(name);
            dictaat.Pages = _directory.GetFilesSummary(_pathHelper.PagesPath(name));
            dictaat.Menu = _menuFactory.GetMenu(name);

            return dictaat;
        }

        public Dictaat CreateDictaat(string name, string template = null)
        {
            //Default value van template is 'default'
            string pathTemplate = _pathHelper.DirectoryTemplatePath(template == null ? "default" : template);
            string pathNew = _pathHelper.DictaatPath(name);

            if (_directory.Exists(pathNew)){
                return null;
            }

            _directory.CopyDirectory(pathNew, pathTemplate);

            return this.GetDictaat(name);
              
        }

        /// <summary>
        /// returneed nu nog een lijst van Directory Summary.
        /// Dit kunnen we in de toekomst wellicht vervangen door een lijst van Dictaat voor meer info.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DirectorySummary> GetDictaten()
        {
            return _directory.GetDirectoriesSummary(_pathHelper.DictatenPath());
        }
    }
}
