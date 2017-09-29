using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.Assignments;

namespace Webdictaat.Api.ViewModels.Participant
{
    public class ParticipantVM
    {
        public UserVM User { get; set; }
        public int[] AssignmentIds { get; set; }
        public double AverageCompletion { get; set; }
        public double ComparedToAverage { get; set; }

        public double CompletedAssignments { get; set; }
        public int RecievedPoints { get; internal set; }
    }
}
