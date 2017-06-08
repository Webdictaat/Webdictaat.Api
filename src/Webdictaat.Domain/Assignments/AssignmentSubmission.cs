using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.User;

namespace Webdictaat.Domain.Assignments
{
    public class AssignmentSubmission
    {
        [Required]
        public int AssignmentId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime Timestamp { get; set; }

        public int PointsRecieved { get; set; }
    }
}
