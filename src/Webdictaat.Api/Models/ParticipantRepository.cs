using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Api.ViewModels.Participant;
using Webdictaat.Data;
using Webdictaat.Domain;
using Webdictaat.Domain.User;

namespace Webdictaat.Api.Models
{
    public interface IParticipantRepository
    {
        IEnumerable<UserVM> GetParticipants(string dictaatName);
        IEnumerable<GroupVM> GetGroups(string dictaatName);
        bool Join(string dictaatName, string group, string userId);
        ParticipantVM GetParticipant(string dictaatName, string email);
        IEnumerable<GroupVM> CreateGroup(string dictaatName, GroupVM group);
        IEnumerable<GroupVM> CreateGroups(string dictaatName, IEnumerable<GroupVM> groups);

        IEnumerable<GroupVM> RemoveGroup(string dictaatName, string group);
    }

    public class ParticipantRepository : IParticipantRepository
    {
        private WebdictaatContext _context;

        public ParticipantRepository(
            WebdictaatContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
        }

        public IEnumerable<UserVM> GetParticipants(string dictaatName)
        {
            //assignments used to calculate total points
            var assignmentIds = _context.Assignments
                .Where(a => a.DictaatDetailsId == dictaatName)
                .Select(a => a.Id).ToArray();

            var participants = _context.DictaatSession
             .Include("Participants.User.AssignmentSubmissions")
             .FirstOrDefault(s => s.DictaatDetailsId == dictaatName && s.EndedOn == null)
             .Participants.Select(p => new UserVM(p.User, assignmentIds, p.Group)).ToList();

            return participants.OrderByDescending(p => p.Points);
        }

        public IEnumerable<GroupVM> GetGroups(string dictaatName)
        {
            var participants = this.GetParticipants(dictaatName);

            var groups = _context.DictaatGroup
                .Include(dg => dg.Participants)
                .Where(d => d.DictaatName == dictaatName)
                .Select(d => new GroupVM(d.Name, 
                    //get all the users from the full participant list
                    participants.Where(p => d.Participants.Any(gp => gp.UserId == p.Id)).ToList()))
                .OrderByDescending(g => g.Points);

            return groups;
        }

        /// <summary>
        /// Join the current session of a dictaat
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <param name="groupName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool Join(string dictaatName, string groupName, string userId)
        {

            var currentSession = _context.DictaatSession
                .Include(s => s.Participants)
                .FirstOrDefault(s => s.EndedOn == null && s.DictaatDetailsId == dictaatName);

            var group = _context.DictaatGroup.FirstOrDefault(dg => dg.DictaatName == dictaatName && dg.Name == groupName);
                            
            if (currentSession.Participants.Any(p => p.UserId == userId))
            {
                return false; //already in the partcipant list
            }
            else
            {
                currentSession.Participants.Add(new DictaatSessionUser()
                {
                    UserId = userId,
                    Group = group != null ? group.Name : null,
                    DictaatName = group != null ? currentSession.DictaatDetailsId : null
                });
                _context.SaveChanges();
                return true; //Joined this ditaat :D
            }
        }

        public ParticipantVM GetParticipant(string dictaatName, string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            var assignmentIds = getAssignmentIds(dictaatName);

            //completed assignmetnns
            var myAssignments = _context.AssignmentSubmissions
                .Where(a => assignmentIds.Contains(a.AssignmentId) && a.UserId == user.Id)
                .ToList();

            var myCompletion = myAssignments.Where(a => a.Accepted).Count();
            var myPending = myAssignments.Where(a => !a.Accepted).Count();

            var myPoints = myAssignments.Where(a => a.Accepted).Sum(a => a.PointsRecieved);

            //count sumbissions
            double submissionCount = _context.AssignmentSubmissions
                .Where(a => assignmentIds.Contains(a.AssignmentId))
                .Count();

            var dictaatSessionId = _context.DictaatSession
                .FirstOrDefault(ds => ds.DictaatDetailsId == dictaatName && ds.EndedOn == null).Id;

            //count participants
            double participantCount = _context.DictaatSessionUser
                .Where(dsu => dsu.DictaatSessionId == dictaatSessionId).Count();

            double averageCompletion = submissionCount / participantCount;


            //completed quizes
            //not yet added, we first need to rework quizes

            return new ParticipantVM()
            {
                AssignmentIds = assignmentIds,
                RecievedPoints = myPoints,
                CompletedAssignments = myCompletion,
                PendingAssignments = myPending,
                AverageCompletion = averageCompletion,
                ComparedToAverage = ((myCompletion - averageCompletion) / averageCompletion) * 100
            };
        }

        /// <summary>
        /// get all assignment ids
        /// </summary>
        /// <param name="dictaatName"></param>
        /// <returns></returns>
        private int[] getAssignmentIds(string dictaatName) {
            return _context.Assignments
                 .Where(a => a.DictaatDetailsId == dictaatName)
                 .Select(a => a.Id).ToArray(); 
        }

        public IEnumerable<GroupVM> CreateGroup(string dictaatName, GroupVM group)
        {
            _context.DictaatGroup.Add(new DictaatGroup { DictaatName = dictaatName, Name = group.GroupName });
            _context.SaveChanges();
            return this.GetGroups(dictaatName);
        }

        public IEnumerable<GroupVM> RemoveGroup(string dictaatName, string groupName)
        {
            var group = _context.DictaatGroup
                .Include(dg => dg.Participants)
                .FirstOrDefault(dg => dg.DictaatName == dictaatName && dg.Name == groupName);

            //remove all participants
            group.Participants.ToList().ForEach(p => _context.DictaatSessionUser.Remove(p));

            //remove the group itself
            _context.DictaatGroup.Remove(group);

            _context.SaveChanges();

            return this.GetGroups(dictaatName);
        }

        public IEnumerable<GroupVM> CreateGroups(string dictaatName, IEnumerable<GroupVM> groups)
        {
            groups.ToList().ForEach(g =>
            {
                _context.DictaatGroup.Add(new DictaatGroup { DictaatName = dictaatName, Name = g.GroupName });
            });
            _context.SaveChanges();
            return this.GetGroups(dictaatName);
        }
    }
}
