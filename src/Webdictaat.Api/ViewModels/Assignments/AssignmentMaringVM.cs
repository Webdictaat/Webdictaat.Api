using System.Collections.Generic;
using Webdictaat.Api.ViewModels.Assignments;
using Webdictaat.Domain.Assignments;
using Webdictaat.Domain.User;
using System.Linq;

namespace Webdictaat.Api.ViewModels
{
    public class AssignmentMarkingVM 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Metadata { get; set; }

        public int Points { get; set; }

        public IEnumerable<AssignmentSubmissionVM> Submissions { get; }

        public AssignmentMarkingVM(Assignment assignment) 
        {
            this.Id = assignment.Id;
            this.Title = assignment.Title;
            this.Description = assignment.Description;
            this.Metadata = assignment.Metadata;
            this.Points = assignment.Points;
            this.Submissions = assignment.Attempts.ToList().Select(s => new AssignmentSubmissionVM(s));
        }

        //if(submissions[userId]
    }
}