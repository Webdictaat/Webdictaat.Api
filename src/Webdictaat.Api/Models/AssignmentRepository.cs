using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.Services;
using Webdictaat.Api.ViewModels.Assignments;
using Webdictaat.Data;
using Webdictaat.Domain.Assignments;

namespace Webdictaat.Api.Models
{
    public interface IAssignmentRepository
    {
        AssignmentVM GetAssignment(int assignmentId, string userId = null);

        AssignmentVM CreateAssignment(string dictaatName, AssignmentFormVM form);

        /// <summary>
        /// Complete a assignment. 
        /// Admins can complete an assignment, or users that know the assignment secret
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="assignmentId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        AssignmentSubmissionVM CompleteAssignment(int assignmentId, string userId, bool accepted);
        AssignmentVM CompleteAssignment(int assignmentId, string email, string token, string userId);
        IEnumerable<AssignmentVM> GetAllAssignments(string dictaatName, string userId);
        AssignmentVM UpdateAssignment(string dictaatName, int assignmentId, AssignmentFormVM form);
        AssignmentVM DeleteAssignment(string dictaatName, int assignmentId);
        bool UndoCompleteAssignment(int assignmentId, string userId);
        
    }

    public class AssignmentRepository : IAssignmentRepository
    {
        private WebdictaatContext _context;
        private ISecretService _secretService;

        public AssignmentRepository(WebdictaatContext context, ISecretService secretService)
        {
            _context = context;
            _secretService = secretService;
        }

        public AssignmentSubmissionVM CompleteAssignment(int assignmentId, string userId, bool accepted)
        {
            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == assignmentId);
            return new AssignmentSubmissionVM(completeAssignment(assignment, userId, accepted));
 
        }

        public AssignmentVM CompleteAssignment(int assignmentId, string email, string token, string userId)
        {
            Assignment assignment = _context.Assignments.FirstOrDefault(a => a.Id == assignmentId);

            if (assignment != null)
            {
                var assignmentToken = _secretService.GetAssignmentToken(email, assignment.ExternalId, assignment.AssignmentSecret);
                if(token == assignmentToken)
                {
                    completeAssignment(assignment, userId, true);
                }
          
                return GetAssignment(assignmentId, userId);
            }

            return null;
        }

        private AssignmentSubmission completeAssignment(Assignment assignment, string userId, bool accepted) {

            var mySubmission = _context.AssignmentSubmissions.FirstOrDefault(a => a.UserId == userId && a.AssignmentId == assignment.Id);

            if (mySubmission != null)
            {
                mySubmission.Accepted = accepted;
            }
            else
            {
                var submission = new AssignmentSubmission()
                {
                    AssignmentId = assignment.Id,
                    UserId = userId,
                    Timestamp = DateTime.Now,
                    PointsRecieved = assignment.Points,
                    Accepted = accepted
                };

                _context.AssignmentSubmissions.Add(submission);
                mySubmission = submission;
            }

            _context.SaveChanges();
            return mySubmission;
        }

      
        public AssignmentVM CreateAssignment(string dictaatName, AssignmentFormVM form)
        {
            var assignment = form.ToPoco();
            assignment.DictaatDetailsId = dictaatName;
            _context.Assignments.Add(assignment);
            _context.SaveChanges();

            return new AssignmentVM(assignment);
        }

        public AssignmentVM GetAssignment(int assignmentId, string userId = null)
        {
            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == assignmentId);
            var response = new AssignmentVM(assignment);

            if(userId != null)
            {
                var submission = _context.AssignmentSubmissions.FirstOrDefault(a => a.UserId == userId && a.AssignmentId == assignmentId);

                if(submission != null)
                    response.MySubmission = new AssignmentSubmissionVM(submission); 
            }

            return response;

        }

        public IEnumerable<AssignmentVM> GetAllAssignments(string dictaatName, string userId)
        {
            var assignments = _context.Assignments
                .Include(a => a.Attempts)
                .Where(a => a.DictaatDetailsId == dictaatName)
                .ToList();

            return assignments.Select(a => new AssignmentVM(a));
        }

        public AssignmentVM UpdateAssignment(string dictaatName, int assignmentId, AssignmentFormVM form)
        {
            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == assignmentId);

            if (assignment == null)
                return null;

            assignment.Title = form.Title;
            assignment.Level = (AssignmentLevel)Enum.Parse(typeof(AssignmentLevel), form.Level);
            assignment.Points = form.Points;
            assignment.Metadata = form.Metadata;
            assignment.Description = form.Description;
            _context.SaveChanges();

            return new AssignmentVM(assignment);



        }

        public AssignmentVM DeleteAssignment(string dictaatName, int assignmentId)
        {
            var assignment = _context.Assignments.FirstOrDefault(a => a.Id == assignmentId);

            if (assignment == null)
                return null;

            _context.Remove(assignment);
            _context.SaveChanges();
            return new AssignmentVM(assignment);

        }

        public bool UndoCompleteAssignment(int assignmentId, string userId)
        {
            var submission = _context.AssignmentSubmissions
                .FirstOrDefault(a => a.AssignmentId == assignmentId && a.UserId == userId);

            if (submission == null)
                return false;

            _context.Remove(submission);
            _context.SaveChanges();
            return true;
        }
    }
}
