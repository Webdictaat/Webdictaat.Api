﻿using System.Collections.Generic;
using Webdictaat.Api.ViewModels.Assignments;
using Webdictaat.Domain.Assignments;
using Webdictaat.Domain.User;
using System.Linq;

namespace Webdictaat.Api.ViewModels
{
    public class AssignmentMaringVM 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Metadata { get; set; }

        public int Points { get; set; }

        public IEnumerable<string> SubmittedBy { get; set; }

        public AssignmentMaringVM(IEnumerable<ApplicationUser> participants, Assignment assignment) 
        {
            this.Id = assignment.Id;
            this.Title = assignment.Title;
            this.Description = assignment.Description;
            this.Metadata = assignment.Metadata;
            this.Points = assignment.Points;
            this.SubmittedBy = assignment.Attempts.Select(a => a.UserId);
        }

        //if(submissions[userId]
    }
}