using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;
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
        public string Name { get; set; }

        public double Points { get; set; }

        public UserVM()
        {

        }

        public UserVM(ApplicationUser p, Boolean showPrivateInfo =false)
        {
            this.Id = p.Id;
            this.Email = showPrivateInfo ? p.Email : null; //avg
            this.UserName = p.UserName;
            this.Name = p.FullName;
        }

        public UserVM(DictaatSessionUser p) : this(p.User) { }

        /// <summary>
        /// Constructor with list of assignments to calculate total points;
        /// </summary>
        /// <param name="p"></param>
        /// <param name="assignmentIds"></param>
        public UserVM(ApplicationUser user, int[] assignmentIds) 
            : this(user)
        {
            this.Points = user.AssignmentSubmissions
                .Where(a => assignmentIds.Contains(a.AssignmentId) && a.Accepted == true)
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

        /// <summary>
        /// Constructor with a list of assignmetns and a group the user belongs to
        /// </summary>
        /// <param name="user"></param>
        /// <param name="assignmentIds"></param>
        /// <param name="group"></param>
        public UserVM(ApplicationUser user, int[] assignmentIds, DictaatGroup group)
            : this(user, assignmentIds)
        {
            this.Group = group != null ? group.Name : null;
        }



    }
}
