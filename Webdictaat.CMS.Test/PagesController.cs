using Moq;
using Webdictaat.Api.Controllers;
using Webdictaat.Api.Models;
using Webdictaat.Api.ViewModels;
using Xunit;

namespace Webdictaat.Api.Test
{
    public class PagesControllerTest
    {
        private PagesController _controller;
        private Mock<IPageRepository> _pageRepoMock;
        private Mock<IMenuRepository> _menuRepo;

        public PagesControllerTest()
        {
            _pageRepoMock = new Mock<IPageRepository>();
            _menuRepo = new Mock<IMenuRepository>();
            _controller = new PagesController(_pageRepoMock.Object, _menuRepo.Object);
        }

        [Fact]
        public void PagesController_Post_Success()
        {
            //arrange
            var form = new DictaatPageForm()
            {
                SubMenu = "a",
                Page = new DictaatPage()
                {
                    Name = "b",
                    Source = "c"
                }
            };
            //act
            _controller.Post("d", form);

            //assert
            _pageRepoMock.Verify(p =>
                p.CreateDictaatPage(
                    It.Is<string>(s => s == "d"),
                    It.IsAny<DictaatPage>()
                ), Times.Once());
        }

      
    }
}