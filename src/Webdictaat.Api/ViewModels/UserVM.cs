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
        private ApplicationUser user;
        public string Group { get; set; }


        public string Email { get; set; }
        public string Id { get; private set; }
        public string UserName { get; set; }

        public double Points { get; set; }

        public UserVM()
        {

        }

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
        public UserVM(ApplicationUser user, int[] assignmentIds) 
            : this(user)
        {
            this.Points = user.AssignmentSubmissions
                .Where(a => assignmentIds.Contains(a.AssignmentId))
                .Sum(a => a.PointsRecieved);
        }

        /// <summary>
        /// Constructor with a list of assignmetns and a group the user belongs to
        /// </summary>
        /// <param name="user"></param>
        /// <param name="assignmentIds"></param>
        /// <param name="group"></param>
        public UserVM(ApplicationUser user, int[] assignmentIds, string group) 
            : this(user, assignmentIds)
        {
            this.Group = group;
        }

    }
}
