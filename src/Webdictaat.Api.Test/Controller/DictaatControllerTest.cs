using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Api.Controllers;
using Webdictaat.Api.Models;
using Webdictaat.Api.ViewModels;
using Webdictaat.Data;
using Webdictaat.Domain;
using Webdictaat.Domain.User;
using Xunit;
using Webdictaat.Api.Services;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Webdictaat.Core;
using Moq;

namespace Webdictaat.Api.Test.Controller
{
    public class DictaatControllerTest : BaseTestController,  IDisposable
    {
        Webdictaat.Api.Controllers.DictatenController _c;
        Mock<IDictaatFactory> _dictaatFactory;

        public DictaatControllerTest()
        {
            _context.Database.BeginTransaction();

            _c = new DictatenController(_dictaatRepo, null, base.am.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new TestPrincipal(new Claim[]{
                            new Claim("name", "ssmulder")
                        })
                    }
                }
            };
        }

        public void Dispose()
        {
            _context.Database.RollbackTransaction();
        }

        [Fact]
        public void Should_Get_Dictaat()
        {
            //arrange
            //act
            var response = _c.Get("Test");

            //assert
            Assert.NotNull(response.Result);
            Assert.Equal("Test", response.Result.Name);

        }

        [Fact]
        public void Should_Copy_Dictaat()
        {
            //arrange

            //act
            var response = _c.CopyDictaat("Test", new CopyDictaatForm()
            {
                Dictaat = new DictaatForm() {  Name = "Test2" }
            });

            //assert
            Assert.NotNull(response);
            Assert.Equal("Test2", response.Name);

            var newDictaat = _context.DictaatDetails
                .Include(dd => dd.Assignments)
                .Include(dd => dd.DictaatOwner)
                .Include(dd => dd.Polls)
                .Include(dd => dd.Sessions)
                .Include("Quizes.Questions.Question.Answers")
                .FirstOrDefault(d => d.Name == "Test2");

            Assert.NotNull(newDictaat);
            Assert.Equal(2, newDictaat.Assignments.Count());
            Assert.Equal(4, _context.Assignments.Count());
            Assert.Equal(1, newDictaat.Quizes.Count());
            Assert.Equal(2, _context.Quizes.Count());
            Assert.Equal(1, newDictaat.Sessions.Count());
            Assert.Equal(2, _context.Sessions.Count());
            Assert.Equal(1, newDictaat.Polls.Count());
            Assert.Equal(2, _context.Polls.Count());





        }
    }
}



    