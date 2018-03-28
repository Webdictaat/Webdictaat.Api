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
        void DeleteDictaaat(string dictaatPath);
        Dictaat CopyDictaat(string dictaatName, DictaatDetails newDictaatName);
    }

    public class DictaatFactory : IDictaatFactory
    {
        private PathHelper _pathHelper;

        private Core.IDirectory _directory;
        private Core.IFile _file;
        private Core.IMenuFactory _menuFactory;
        private IJson _json;

        public DictaatFactory(ConfigVariables configVariables, Core.IDirectory directory, Core.IFile file, Core.IJson json)
        {
            _directory = directory;
            _pathHelper = new PathHelper(configVariables);
            _menuFactory = new MenuFactory(configVariables, file);
            _json = json;
        }

        public Dictaat GetDictaat(string name)
        {
            Dictaat dictaat = new Dictaat();
            dictaat.Name = name;
            dictaat.Location = _pathHelper.DictaatPath(name);
            dictaat.Pages = _directory.GetFilesSummary(_pathHelper.PagesPath(name));
            dictaat.MenuItems = _menuFactory.GetMenu(name);

            return dictaat;
        }

        public Dictaat CreateDictaat(string name, string template = null)
        {
            //Default value van template is 'default'
            string pathTemplate = _pathHelper.DirectoryTemplatePath(template == null ? "default" : template);
            string pathNew = _pathHelper.DictaatPath(name);
            string pathNewConfig = _pathHelper.DictaatConfigPath(name);

            if (_directory.Exists(pathNew))
            {
                return null;
            }

            //copy template
            _directory.CopyDirectory(pathNew, pathTemplate);

            //edit custom files
            var dictaatConfig = _json.ReadFile(pathNewConfig);
            dictaatConfig["name"] = name;
            _json.EditFile(pathNewConfig, dictaatConfig);

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

        public void DeleteDictaaat(string dictaatPath)
        {
            _directory.DeleteDirectory(dictaatPath);
        }

        public Dictaat CopyDictaat(string dictaatName, DictaatDetails newDictaat)
        {
            var oldDir = _pathHelper.DictaatPath(dictaatName);
            var newDir = _pathHelper.DictaatPath(newDictaat.Name);

            //This is where the copy happens
            _directory.CopyDirectory(newDir, oldDir);

            //edit custom files
            string pathNewConfig = _pathHelper.DictaatConfigPath(newDictaat.Name);
            var dictaatConfig = _json.ReadFile(pathNewConfig);
            dictaatConfig["name"] = newDictaat.Name;
            _json.EditFile(pathNewConfig, dictaatConfig);

            return GetDictaat(newDictaat.Name);
        }
    }
}
