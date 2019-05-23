using System;
using System.Collections.Generic;
using System.Text;
using Webdictaat.Api.ViewModels;
using Xunit;
using System.Linq;
using Moq;
using Microsoft.EntityFrameworkCore;
using Webdictaat.Data;
using Webdictaat.Api.Test.Controller;

namespace Webdictaat.Api.Test.Repository
{
    public class DictaatRepositoryTest : BaseTestController
    {
        Webdictaat.Api.Models.DictaatRepository _r;
        WebdictaatContext context;

        public DictaatRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<WebdictaatContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                  .Options;

            context = new WebdictaatContext(options);
            context.DictaatDetails.Add(new Domain.DictaatDetails() { Name = "Test 1", DictaatOwnerId = "1" });
            context.DictaatDetails.Add(new Domain.DictaatDetails() { Name = "Test 2", DictaatOwnerId = "2" });
            context.Users.Add(new Domain.User.ApplicationUser() { Id = "1", FullName = "User A" });
            context.Users.Add(new Domain.User.ApplicationUser() { Id = "2", FullName = "User B" });
            context.SaveChanges();
            
            _r = new Models.DictaatRepository(_config.Object, null, null, context);
        }

        [Fact]
        public void Should_Get_All_Dictaten_As_Admin()
        {
            //arrange

            //act
            IEnumerable<DictaatSummary> dictaten = _r.GetDictaten("1", true);

            //assert
            Assert.NotNull(dictaten);
            Assert.Equal(2, dictaten.Where(d => d.CanEdit).Count());
        }

        [Fact]
        public void Should_Get_My_Dictaten_As_User()
        {
            //arrange

            //act
            IEnumerable<DictaatSummary> dictaten = _r.GetDictaten("1");

            //assert
            Assert.NotNull(dictaten);
            Assert.Single(dictaten.Where(d => d.CanEdit));
        }


    }
}
