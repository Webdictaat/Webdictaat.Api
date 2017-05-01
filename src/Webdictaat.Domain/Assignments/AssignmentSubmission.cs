using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Domain.Assignments
{
    public class AssignmentSubmission
    {
        [Required]
        public int AssignmentId { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime Timestamp { get; set; }

        public int PointsRecieved { get; set; }
    }
}
