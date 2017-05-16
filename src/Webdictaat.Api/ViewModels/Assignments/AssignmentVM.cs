using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.Assignments;

namespace Webdictaat.Api.ViewModels.Assignments
{
    public class AssignmentVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Metadata { get; set; }

        public int Points { get; set; }

        public ICollection<AssignmentSubmission> Submissions { get; set; }
 
        public AssignmentSubmission MySubmission { get; set; }

        public int SubmissionCount { get; set; }

        public AssignmentVM()
        {

        }

        public AssignmentVM(Assignment assignment)
        {
            this.Id = assignment.Id;
            this.Title = assignment.Title;
            this.Description = assignment.Description;
            this.Metadata = assignment.Metadata;
            this.Points = assignment.Points;
            this.Submissions = assignment.Attempts;

            if (this.Submissions != null)
            {
                this.SubmissionCount = this.Submissions.GroupBy(s => s.UserId).Count();
            }
            
        }
    }
}
