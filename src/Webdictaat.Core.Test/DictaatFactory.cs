using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Core;
using Xunit;

namespace Webdictaat.Core.Test
{
    public class DictaatFactoryTest
    {
        DictaatFactory _fac;
        ConfigVariables _config;

        public DictaatFactoryTest()
        {
            _config = new ConfigVariables()
            {
                DictaatRoot = "./resources",
                PagesDirectory = "pages",
                MenuConfigName = "menu-config.json",
                DictaatConfigName = "dictaat.config.json"
            };

            _fac = new DictaatFactory(_config, new Directory(), new File(), new Core.Json());
        }

        [Fact]
        public void DictaatFactory_CreateDictaat_Success()
        {
            //arrange
            _fac.CreateDictaat("a");

            //assert
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(_config.DictaatRoot + "/dictaten/a");
            Assert.True(dir.Exists);
            Assert.Equal(2, dir.GetFiles().Length);


            //clean up
            System.IO.Directory.Delete(_config.DictaatRoot + "/dictaten/a", true);
        }





    }
}
