using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Webdictaat.Core.Test
{
    public class MenuFactoryTest
    {
        private Webdictaat.Core.IMenuFactory _menuFactory;
        private Mock<IFile> _fileMock;

        public MenuFactoryTest()
        {
            var config = new ConfigVariables()
            {
                DictaatRoot = "C:\\webdictaat.test",
                PagesDirectory = "pages",
                MenuConfigName = "menu-config.json"
            };

            _fileMock = new Mock<IFile>();
            _menuFactory = new Webdictaat.Core.MenuFactory(config, _fileMock.Object);
        }

        [Fact]
        public void MenuFactoryTest_GetMenu_Success()
        {

            //1. arrange
            string menuJson = "{ 'name': 'a', 'SubMenus': ["
                + " { 'name': 'b', 'MenuItems': [ { 'name' : 'c' , 'url': 'd' } ] } "
                + "], 'MenuItems': [ { 'name' : 'e' , 'url': 'f' }  ] }";

            _fileMock.Setup(f => f.TryReadFile(It.IsAny<string>()))
                .Returns(menuJson);
      
            //2. act
            Domain.Menu menu = _menuFactory.GetMenu("");

            //3. assert
            Assert.Equal("a", menu.Name);
            Assert.Equal("b", menu.SubMenus.FirstOrDefault().Name);
            Assert.Equal("c", menu.SubMenus.FirstOrDefault().MenuItems.FirstOrDefault().Name);
            Assert.Equal("d", menu.SubMenus.FirstOrDefault().MenuItems.FirstOrDefault().Url);
            Assert.Equal("e", menu.MenuItems.FirstOrDefault().Name);
            Assert.Equal("f", menu.MenuItems.FirstOrDefault().Url);


        }

        [Fact]
        public void MenuFactoryTest_EditMenu_Success()
        {

            //1. arrange
            var menu = new Domain.Menu()
            {
                Name = "a",
                SubMenus = new List<Domain.Menu>() {
                    new Domain.Menu() {
                        Name = "b",
                        MenuItems = new List<Domain.MenuItem>()
                        {
                            new Domain.MenuItem() { Name = "c", Url = "d" },
                        }
                    },
                },
                MenuItems = new List<Domain.MenuItem>()
                {
                    new Domain.MenuItem() { Name = "e", Url = "f" },
                }
            };

            string menuJson = "{ 'name': 'a', 'SubMenus': ["
                + " { 'name': 'b', 'MenuItems': [ { 'name' : 'c' , 'url': 'd' } ] } "
                + "], 'MenuItems': [ { 'name' : 'e' , 'url': 'f' }  ] }";

            _fileMock.Setup(f => f.TryEditFile(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            //2. act
            Domain.Menu result = _menuFactory.EditMenu("3", menu);

            //3. assert
            _fileMock.Verify(f => f.TryEditFile(
                It.Is<string>(s => s == "1\\3\\2"),
                It.IsAny<string>()
            ), Times.Once());

            Assert.Equal<Domain.Menu>(menu, result);
        }
    }
}
