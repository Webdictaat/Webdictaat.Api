using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Models;
using Webdictaat.Api.Services;
using Webdictaat.Api.ViewModels.Assignments;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.Controllers
{
    [Route("api/dictaten/{dictaatName}/[controller]")]
    public class AssignmentController : Controller
    {
        private IAssignmentRepository _assignmentRepo;
        private IAuthorizeService _authorizeService;
        private UserManager<ApplicationUser> _userManager;
         

        public AssignmentController(
            IAuthorizeService authorizeService,
            IAssignmentRepository assignmentRepo,
            UserManager<Domain.User.ApplicationUser> userManager)
        {
            _assignmentRepo = assignmentRepo;
            _userManager = userManager;
            _authorizeService = authorizeService;
        }

        [HttpGet("{assignmentId}")]
        public AssignmentVM Get(string dictaatName, int assignmentId)
        {
            string userId = "";
            if(HttpContext.User != null)
            {
                userId = _userManager.GetUserId(HttpContext.User);
            }
          
            return _assignmentRepo.GetAssignment(assignmentId, userId);
        }

        [HttpPost]
        public AssignmentVM Post(string dictaatName, [FromBody] AssignmentFormVM form)
        {
            return _assignmentRepo.CreateAssignment(dictaatName, form);
        }

        [HttpPost("{assignmentId}/submissions")]
        [Authorize]
        public async Task<AssignmentVM> PostSubmission(string dictaatName, int assignmentId, [FromBody] AssignmentSubmissionFormVM form)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            if (form.Token != null)
            {
                return _assignmentRepo.CompleteAssignment(assignmentId, userId, form.Token);
            }
            else
            {
                if (!await _authorizeService.IsDictaatOwner(User.Identity.Name, dictaatName))
                {
                    HttpContext.Response.StatusCode = 403;
                    return null;
                }
                else
                {
                    return _assignmentRepo.CompleteAssignment(assignmentId, userId);
                }
            }
        }

    }
}
