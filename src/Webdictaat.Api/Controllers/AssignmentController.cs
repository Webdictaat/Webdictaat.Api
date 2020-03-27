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
    /// <summary>
    /// Assignment Controller
    /// </summary>
    [Route("api/dictaten/{dictaatName}/[controller]")]
    public class AssignmentController :BaseController
    {
        private IAssignmentRepository _assignmentRepo;
        private IAuthorizeService _authorizeService;
        private UserManager<ApplicationUser> _userManager;


        public AssignmentController(
            IAuthorizeService authorizeService,
            IAssignmentRepository assignmentRepo,
            UserManager<Domain.User.ApplicationUser> userManager) : base(authorizeService)
        {
            _assignmentRepo = assignmentRepo;
            _userManager = userManager;
            _authorizeService = authorizeService;
        }

        /// <summary>
        /// Get One
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get List
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public AssignmentVM Post(string dictaatName, [FromBody] AssignmentFormVM form)
        {
            if (!AuthorizeResource(dictaatName))
                return null;

            return _assignmentRepo.CreateAssignment(dictaatName, form);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="assignmentId"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPut("{assignmentId}")]
        public AssignmentVM Put(string dictaatName, int assignmentId, [FromBody] AssignmentFormVM form)
        {
            if (!AuthorizeResource(dictaatName))
                return null;

            return _assignmentRepo.UpdateAssignment(dictaatName, assignmentId, form);
        }

        /// <summary>
        /// Delete assignment
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="assignmentId"></param>
        /// <returns></returns>
        [HttpDelete("{assignmentId}")]
        public AssignmentVM Delete(string dictaatName, int assignmentId)
        {
            if (!AuthorizeResource(dictaatName))
                return null;

            return _assignmentRepo.DeleteAssignment(dictaatName, assignmentId);
        }

        /// <summary>
        /// Delete submission
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="assignmentId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{assignmentId}/submissions/{userId}")]
        [Authorize]
        public Boolean DeleteSubmission(string dictaatName, int assignmentId, string userId)
        {
            if (!AuthorizeResource(dictaatName))
                return false;

            return _assignmentRepo.UndoCompleteAssignment(assignmentId, userId);

        }

        [HttpPost("{assignmentId}/submissions")]
        [Authorize]
        public async Task<object> PostSubmission(string dictaatName, int assignmentId, [FromBody] AssignmentSubmissionFormVM form)
        {
            string userId = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByIdAsync(userId);

            if (form.Token != null)
            {
                //use the user identy name (email)
                return _assignmentRepo.CompleteAssignment(assignmentId, user.Email, form.Token, userId);
            }
            else
            {
                var isContributer = await _authorizeService.IsDictaatContributer(userId, dictaatName);

                if(isContributer)
                {
                    //if you are a contributer, you can choose who has completed the assignment (form data)
                    return _assignmentRepo.CompleteAssignment(assignmentId, form.UserId, isContributer);
                }
                else
                {
                    return _assignmentRepo.CompleteAssignment(assignmentId, userId, isContributer);

                }

            }
        }

    }
}
