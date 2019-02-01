using System;
using System.Collections.Generic;
using System.Text;
using Webdictaat.Core;
using Webdictaat.Domain.Assignments;
using Xunit;

namespace Webdictaat.Domain.Test.Dictaat
{


    public class DictaatFactoryTest : IDisposable
    {
        private DictaatFactory _df;

        public DictaatFactoryTest()
        {
            var relative = System.Reflection.Assembly.GetExecutingAssembly().Location;

            var dir = new Directory();
            // Get the subdirectories for the specified directory.
            if (new System.IO.DirectoryInfo("C:\\Temp").Exists)
            {
                dir.DeleteDirectory("C:\\Temp");
            }
            dir.CreateDirectory("C:\\Temp");

            _df = new DictaatFactory(new ConfigVariables()
            {
                DictaatRoot = relative + "C:\\Temp",
                DictatenDirectory = "dictaten",
                TemplatesDirectory = "templates",
                PagesDirectory = "pages",
                MenuConfigName = "nav-menu.json",
                ImagesDirectory = "images"
            }, dir, new Core.File(), new Core.Json());
        }


        [Fact]
        public void CopyDictaat_Success()
        {
            //arrange

            //act
            //_df.CopyDictaat("test", "test2");

            //asserrt

        }

        [Fact]
        public void UpdatePage_Success()
        {
            //arrange
            //var dictaat = new DictaatDetails()
            //{
            //    Assignments = new List<Assignment>()
            //    {
            //        new Assignment(){ Id = 2},
            //    }
            //};

            //string html = "<wd-assignment [aid]='1'></wd-assignment>";

            ////act
            //html = _df.(html, dictaat);

            ////assert
            //Assert.Equal("<wd-assignment [aid]='2'></wd-assignment>", html);
        }

        public void Dispose()
        {
           
        }
    }
}
