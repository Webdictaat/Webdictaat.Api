using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.Assignments;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.ViewModels.Participant
{
    public class ParticipantVM : UserVM
    {
        public ParticipantVM(ApplicationUser p) : base(p){}

        public UserVM User { get; set; }
        public int[] AssignmentIds { get; set; }
        public double AverageCompletion { get; set; }
        public double ComparedToAverage { get; set; }

        public double CompletedAssignments { get; set; }
        public int RecievedPoints { get; internal set; }
        public int PendingAssignments { get; internal set; }
    }
}
