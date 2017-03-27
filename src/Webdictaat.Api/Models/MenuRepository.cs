using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.CMS.ViewModels;
using Webdictaat.Core;

namespace Webdictaat.CMS.Models
{
    public interface IMenuRepository
    {
        List<ViewModels.MenuItem> AddMenuItem(string dictaat, string parentMenu, ViewModels.MenuItem item );
        List<ViewModels.MenuItem> GetMenu(string dictaat);
        List<ViewModels.MenuItem> EditMenu(string dictaat, List<ViewModels.MenuItem> item);
        List<ViewModels.MenuItem> RemoveMenuItem(string dictaatName, string pageName);
    }
    public class MenuRepository : IMenuRepository
    {
        private string _directoryRoot;
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


        public List<ViewModels.MenuItem> GetMenu(string dictaat)
        {
            var menuItems = _menuFactory.GetMenu(dictaat);
            return menuItems.ToList().Select(mi => new ViewModels.MenuItem(mi)).ToList();
        }

        public List<ViewModels.MenuItem> AddMenuItem(string dictaat, string parentMenu, ViewModels.MenuItem item)
        {
            var menu = GetMenu(dictaat);

            if(parentMenu != null)
            {
                MenuItem parent = menu.FirstOrDefault(s => s.Name.Equals(parentMenu));
                if(parent != null)
                {
                    parent.MenuItems.Add(item);
                }
            }
            else
            {
                menu.Add(item);
            }

            return EditMenu(dictaat, menu);
        }


        public List<ViewModels.MenuItem> EditMenu(string dictaat, List<ViewModels.MenuItem> menuItems)
        {
            var newMenuItems = _menuFactory.EditMenu(dictaat, menuItems.Select(mi => mi.ToPoco()));
            if(newMenuItems == null)
            {
                throw new System.IO.FileNotFoundException();
            }
            return newMenuItems.ToList().Select(mi => new ViewModels.MenuItem(mi)).ToList();
        }

        public List<MenuItem> RemoveMenuItem(string dictaatName, string pageName)
        {
            var menuItems = GetMenu(dictaatName);

            menuItems.RemoveAll(m => m.Url == pageName);
            menuItems.ForEach(mi => mi.MenuItems.RemoveAll(smi => smi.Url.Equals(pageName)));

            return EditMenu(dictaatName, menuItems);
        }
    }
}
