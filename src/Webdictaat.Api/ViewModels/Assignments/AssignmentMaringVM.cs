using System.Collections.Generic;
using Webdictaat.Api.ViewModels.Assignments;
using Webdictaat.Domain.Assignments;
using Webdictaat.Domain.User;
using System.Linq;

namespace Webdictaat.Api.ViewModels
{
    public class AssignmentMaringVM : AssignmentVM
    {
        public Dictionary<string, AssignmentSubmission> Submissions { get; set; }

        public AssignmentMaringVM(IEnumerable<ApplicationUser> participants, Assignment assignment) : base(assignment)
        {
            this.Submissions = new Dictionary<string, AssignmentSubmission>();

            foreach(var p in participants)
            {
                this.Submissions.Add(p.Id, assignment.Attempts.FirstOrDefault(a => a.UserId == p.Id));
            }
        }

        //if(submissions[userId]
    }
}