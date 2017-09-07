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
        //private Mock<IFile> _fileMock;

        public MenuFactoryTest()
        {
            var config = new ConfigVariables()
            {
                DictaatRoot = "C:\\webdictaat.test",
                PagesDirectory = "pages",
                MenuConfigName = "menu-config.json"
            };

            //_fileMock = new Mock<IFile>();
            //_menuFactory = new Webdictaat.Core.MenuFactory(config, _fileMock.Object);
        }

        [Fact]
        public void MenuFactoryTest_GetMenu_Success()
        {

            //1. arrange
            string menuJson = @"
                [
                    { 'Name': 'A', 'Url': 'B' },
                    { 'Name': 'C', 'MenuItems': [ 
                        { 'Name': 'D', 'Url': 'E' },
                        { 'Name': 'F', 'Url': 'G' }
                    ]},
                    { 'Name': 'H', 'Url': 'I' },
                ]
            ";

            //_fileMock.Setup(f => f.TryReadFile(It.IsAny<string>()))
            //    .Returns(menuJson);
      
            //2. act
            List<Domain.MenuItem> menu = _menuFactory.GetMenu("").ToList();

            //3. assert
            Assert.Equal("A", menu[0].Name);
            Assert.Equal("B", menu[0].Url);
            Assert.Equal("C", menu[1].Name);
            Assert.Equal("D", menu[1].MenuItems.ToArray()[0].Name);
            Assert.Equal("E", menu[1].MenuItems.ToArray()[0].Url);
            Assert.Equal("F", menu[1].MenuItems.ToArray()[1].Name);
            Assert.Equal("G", menu[1].MenuItems.ToArray()[1].Url);
            Assert.Equal("H", menu[2].Name);
            Assert.Equal("I", menu[2].Url);


        }
    }
}
