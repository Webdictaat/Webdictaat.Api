using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.ViewModels
{
    public class UserVM
    {
        private IQueryable<int> assignmentIds;

        public UserVM(ApplicationUser p)
        {
            this.Id = p.Id;
            this.Email = p.Email;
            this.UserName = p.UserName;
        }

        /// <summary>
        /// Constructor with list of assignments to calculate total points;
        /// </summary>
        /// <param name="p"></param>
        /// <param name="assignmentIds"></param>
        public UserVM(ApplicationUser p, int[] assignmentIds) : this(p)
        {
            this.Points = p.AssignmentSubmissions
                .Where(a => assignmentIds.Contains(a.AssignmentId))
                .Sum(a => a.PointsRecieved);
        }

        public string Email { get; set; }
        public string Id { get; private set; }
        public string UserName { get; set; }

        public double Points { get; set; }
    }
}
