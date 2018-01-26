using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Webdictaat.Api.ViewModels;
using Xunit;

namespace Webdictaat.Api.Test.ViewModels
{
    public class Issue43_NameContainsHash
    {
        [Fact]
        public void Name_Is_Valid_Test()
        {
            //1. arrange
            var model = new DictaatPageSummary();
            model.Name = "ABCD";

            //2. act
            var result = Validator.TryValidateObject(model, new ValidationContext(model, null, null), null, true);

            //3. assert
            Assert.True(result);
        }

        [Fact]
        public void Name_Is_Invalid_Test()
        {
            //1. arrange
            var model = new DictaatPageSummary();
            model.Name = "AB#CD";

            //2. act
            var result = Validator.TryValidateObject(model, new ValidationContext(model, null, null), null, true);

            //3. assert
            Assert.False(result);
        }

        [Fact]
        public void Url_Is_Valid_Test()
        {
            //1. arrange
            var model = new DictaatPageSummary();
            model.Url = "ABCD";

            //2. act
            var result = Validator.TryValidateObject(model, new ValidationContext(model, null, null), null, true);

            //3. assert
            Assert.True(result);
        }

        [Fact]
        public void Url_Is_Invalid_Test()
        {
            //1. arrange
            var model = new DictaatPageSummary();
            model.Url = "AB#CD";

            //2. act
            var result = Validator.TryValidateObject(model, new ValidationContext(model, null, null), null, true);

            //3. assert
            Assert.False(result);
        }

    }
}
