using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Api.Controllers;
using Webdictaat.Api.Models;
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
    public class AssignmentControllerTest : BaseTestController, IDisposable
    {
        Webdictaat.Api.Controllers.AssignmentController _c;

        public AssignmentControllerTest()
        {
            _context.Database.BeginTransaction();

            _c = new AssignmentController(base.am.Object, base._assignmentRepo, null)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = _user
                    }
                }
            };
        }

        public void Dispose()
        {
            _context.Database.RollbackTransaction();
        }

        [Fact]
        public void Should_Delete_Assignment()
        {
            //arrange
            var assignment = new Domain.Assignments.Assignment()
            {
                Title = "To be deleted",
                Description = "To be deleted",
                DictaatDetailsId = "Dictaat_1"
            };

            base._context.Assignments.Add(assignment);
            base._context.SaveChanges();

            base.am.Setup(a => a.IsDictaatContributer(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(true));

            //act
            var response = _c.Delete("Dictaat_1", assignment.Id);


            //assert
            Assert.Equal(assignment.Id, response.Id);
        }

        [Fact]
        public void Should_Not_Delete_Assignment()
        {
            //arrange
            //arrange
            var assignment = new Domain.Assignments.Assignment()
            {
                Title = "To be deleted",
                Description = "To be deleted",
                DictaatDetailsId = "Dictaat_1"
            };

            base._context.Assignments.Add(assignment);
            base._context.SaveChanges();


            //act - Wrong dictaat
            var response = _c.Delete("Dictaat_2", assignment.Id);

            //assert
            Assert.Null(response);

            //clean up
            base._context.Assignments.Remove(assignment);
            base._context.SaveChanges();

        }
    }
}



