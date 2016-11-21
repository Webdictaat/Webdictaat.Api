using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Core;

namespace Webdictaat.CMS.Models
{
    public interface IMenuRepository
    {
        ViewModels.Menu AddMenuItem(string dictaat, string subMenu, ViewModels.MenuItem item );
        ViewModels.Menu GetMenu(string dictaat);
        ViewModels.Menu EditMenu(string dictaat, ViewModels.Menu item);
    }
    public class MenuRepository : IMenuRepository
    {
        private string _directoryRoot;
        private string _pagesDirectory;
        private string _menuConfigName;
        private IFile _file;
        private IMenuFactory _menuFactory;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="directory"></param>
        /// <param MenuRepository="dictaatFactory"></param>
        public MenuRepository( IOptions<ConfigVariables> appSettings, IFile file)
        {
            _directoryRoot = appSettings.Value.DictaatRoot;
            _menuConfigName = appSettings.Value.MenuConfigName;
            _file = file;

            //best place to build the factory
            _menuFactory = new MenuFactory(appSettings.Value, file);

        }


        public ViewModels.Menu GetMenu(string dictaat)
        {
            return new ViewModels.Menu(_menuFactory.GetMenu(dictaat));
        }

        public ViewModels.Menu AddMenuItem(string dictaat, string subMenu, ViewModels.MenuItem item)
        {
            var menu = GetMenu(dictaat);

            if(subMenu != null)
            {
                menu.SubMenus.FirstOrDefault(s => s.Name.Equals(subMenu))
                    .MenuItems.Add(item);
            }
            else
            {
                menu.MenuItems.Add(item);
            }

            menu = EditMenu(dictaat, menu);
            return menu;

        }


        public ViewModels.Menu EditMenu(string dictaat, ViewModels.Menu menu)
        {
            var newMenu = _menuFactory.EditMenu(dictaat, menu.ToPoco());
            if(newMenu == null)
            {
                throw new System.IO.FileNotFoundException();
            }
            return new ViewModels.Menu(newMenu);
        }
    }
}
