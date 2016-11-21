using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Core.Extensions;
using Webdictaat.Core.Helper;
using Webdictaat.Domain;


namespace Webdictaat.Core
{
    public interface IMenuFactory
    {

        Domain.Menu GetMenu(string dictaatName);

        Domain.Menu EditMenu(string dictaatName, Domain.Menu menu);
    }

    public class MenuFactory : IMenuFactory
    {
        private PathHelper _pathHelper;

        private Core.IFile _file;

        public MenuFactory(ConfigVariables configVariables,  Core.IFile file)
        {
            _pathHelper = new PathHelper(configVariables);
            _file = file;
        }

        public Menu GetMenu(string dictaatName)
        {
            var path = _pathHelper.MenuConfigPath(dictaatName);
            Menu menu = null;
            string menuString = _file.TryReadFile(path);

            if(menuString != null)
            {
                menu = Newtonsoft.Json.JsonConvert.DeserializeObject<Menu>(menuString);
            }
            return menu;
        }

        public Menu EditMenu(string dictaatName, Menu menu)
        {
            var path = _pathHelper.MenuConfigPath(dictaatName);

            var menuString = Newtonsoft.Json.JsonConvert.SerializeObject(menu, Newtonsoft.Json.Formatting.Indented);
            if (_file.TryEditFile(path, menuString))
            {
                return menu;
            }

            return null;
        }
    }
}
