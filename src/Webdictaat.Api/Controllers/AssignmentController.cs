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


        [HttpGet]
        public IEnumerable<AssignmentVM> Get(string dictaatName)
        {
            string userId = "";
            if (HttpContext.User != null)
            {
                userId = _userManager.GetUserId(HttpContext.User);
            }

            return _assignmentRepo.GetAllAssignments(dictaatName, userId);
        }

        [HttpPost]
        public AssignmentVM Post(string dictaatName, [FromBody] AssignmentFormVM form)
        {
            return _assignmentRepo.CreateAssignment(dictaatName, form);
        }


        [HttpPut("{assignmentId}")]
        public AssignmentVM Put(string dictaatName, int assignmentId, [FromBody] AssignmentFormVM form)
        {
            return _assignmentRepo.UpdateAssignment(dictaatName, assignmentId, form);
        }

        [HttpDelete("{assignmentId}")]
        public AssignmentVM Delete(string dictaatName, int assignmentId)
        {
            return _assignmentRepo.DeleteAssignment(dictaatName, assignmentId);
        }

        [HttpDelete("{assignmentId}/submissions/{userId}")]
        [Authorize]
        public async Task<Boolean> DeleteSubmission(string dictaatName, int assignmentId, string userId)
        {
            if (!await _authorizeService.IsDictaatContributer(User.Identity.Name, dictaatName))
            {
                HttpContext.Response.StatusCode = 403;
                return false;
            }
            else
            {
                return _assignmentRepo.UndoCompleteAssignment(assignmentId, userId);
            }

            
        }

        [HttpPost("{assignmentId}/submissions")]
        [Authorize]
        public async Task<AssignmentVM> PostSubmission(string dictaatName, int assignmentId, [FromBody] AssignmentSubmissionFormVM form)
        {
            string userId = _userManager.GetUserId(HttpContext.User);

            if (form.Token != null)
            {
                //use the user identy name (email)
                return _assignmentRepo.CompleteAssignment(assignmentId, User.Identity.Name, form.Token, userId);
            }
            else
            {
                if (!await _authorizeService.IsDictaatContributer(User.Identity.Name, dictaatName))
                {
                    HttpContext.Response.StatusCode = 403;
                    return null;
                }
                else
                {
                    return _assignmentRepo.CompleteAssignment(assignmentId, form.UserId);
                }
            }
        }

    }
}
