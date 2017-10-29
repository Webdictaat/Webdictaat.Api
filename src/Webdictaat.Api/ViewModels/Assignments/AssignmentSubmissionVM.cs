using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.Assignments;

namespace Webdictaat.Api.ViewModels.Assignments
{
    public class AssignmentSubmissionVM
    {
        public int AssignmentId { get; set; }

        public string UserId { get; set; }

        public DateTime Timestamp { get; set; }

        public int PointsRecieved { get; set; }

        public bool Accepted { get; set; }

        public AssignmentSubmissionVM(AssignmentSubmission s)
        {
            this.Accepted = s.Accepted;
            this.UserId = s.UserId;
            this.Timestamp = s.Timestamp;
            this.PointsRecieved = s.PointsRecieved;
            this.Accepted = s.Accepted;
        }
    }
}
