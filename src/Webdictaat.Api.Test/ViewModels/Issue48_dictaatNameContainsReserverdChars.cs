using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Webdictaat.Api.ViewModels;
using Xunit;

namespace Webdictaat.Api.Test.ViewModels
{
    public class Issue48_dictaatNameContainsReserverdChars
    {
        [Fact]
        public void Name_Is_Valid_Test()
        {
            //1. arrange
            var model = new DictaatForm();
            model.Name = "ABCD1234";

            //2. act
            var result = Validator.TryValidateObject(model, new ValidationContext(model, null, null), null, true);

            //3. assert
            Assert.True(result);
        }

        [Fact]
        public void Name_Is_Invalid_Test()
        {
            //1. arrange
            var model = new DictaatForm();
            model.Name = "AB#CD12345";

            //2. act
            var result = Validator.TryValidateObject(model, new ValidationContext(model, null, null), null, true);

            //3. assert
            Assert.False(result);
        }
        [Fact]
        public void Name_Is_ToLong_Test()
        {
            //1. arrange
            var model = new DictaatForm();
            model.Name = "0123456789ABCDEFGHIJX";

            //2. act
            var result = Validator.TryValidateObject(model, new ValidationContext(model, null, null), null, true);

            //3. assert
            Assert.False(result);
        }

    }
}
