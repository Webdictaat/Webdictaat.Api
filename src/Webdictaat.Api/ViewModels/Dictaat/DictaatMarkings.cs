using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels.Assignments;
using Webdictaat.Domain;
using Webdictaat.Domain.Assignments;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.ViewModels
{
    /// <summary>
    /// This is a view model that contains information to make it easy to show the mark offs on assignments
    /// </summary>
    public class DictaatMarkings
    {
        public List<UserVM> Participants { get; set; }
        public List<AssignmentMaringVM> Assignments { get; internal set; }

        public DictaatMarkings()
        {

        }

        public DictaatMarkings(List<Assignment> assignments, IEnumerable<ApplicationUser> participants)
        {
            this.Participants = participants.Select(p => new ViewModels.UserVM(p)).ToList();
            this.Assignments = assignments.Select(a => new ViewModels.AssignmentMaringVM(participants, a)).ToList();
        }


    }
}
