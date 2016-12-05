using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.CMS.Models;
using Webdictaat.CMS.ViewModels;
using Webdictaat.Core;
using Xunit;

namespace Webdictaat.CMS.Test
{
    public class MenuRepositoryTest
    {
        private IMenuRepository _menuRepo;
        private Mock<IFile> _fileMock;

        public MenuRepositoryTest()
        {
            var optionsMock = new Mock<IOptions<ConfigVariables>>();

            optionsMock.Setup(m => m.Value.DictaatRoot).Returns("a");
            optionsMock.Setup(m => m.Value.MenuConfigName).Returns("b");

            _fileMock = new Mock<IFile>();

            //_menuRepo = new MenuRepository(optionsMock.Object, _fileMock.Object);
            
        }



    }
}
