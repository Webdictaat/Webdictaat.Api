using Moq;
using Webdictaat.Api.Controllers;
using Webdictaat.Api.Models;
using Webdictaat.Api.ViewModels;
using Xunit;

namespace Webdictaat.Api.Test
{
    public class DictatenControllerTest
    {
        private DictatenController _controller;
        private Mock<IDictaatRepository> _dictaatRepoMock;

        public DictatenControllerTest()
        {
            _dictaatRepoMock = new Mock<IDictaatRepository>();
            _dictaatRepoMock.Setup(d => d.CreateDictaat(It.IsAny<string>(), It.IsAny<string>()));
            _controller = new DictatenController(_dictaatRepoMock.Object);
        }

        [Fact]
        public void DictaatController_Post_Success()
        {
            _controller.Post(new DictaatForm() { Name = "a", Template = "b" });

            _dictaatRepoMock.Verify(d => d.CreateDictaat(
                It.Is<string>(n => n == "a"), 
                It.Is<string>(t => t == "b")), 
                Times.Once);


        }


    }
}