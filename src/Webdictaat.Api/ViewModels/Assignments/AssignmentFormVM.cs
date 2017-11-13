using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain.Assignments;

namespace Webdictaat.Api.ViewModels.Assignments
{
    public class AssignmentFormVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Metadata { get; set; }

        public int Points { get; set; }

        public string Level { get; set; }

        public string AssignmentType { get; set; }

        public int CompletedCount { get; set; }

        public AssignmentFormVM()
        {

        }

        public AssignmentFormVM(Assignment assignment)
        {
            this.Id = assignment.Id;
            this.Title = assignment.Title;
            this.Description = assignment.Description;
            this.Metadata = assignment.Metadata;
            this.Points = assignment.Points;
            this.Level = assignment.Level.ToString();
            this.AssignmentType = assignment.AssignmentType.ToString();

            if(assignment.Attempts != null)
                this.CompletedCount = assignment.Attempts.Count();
        }

   
        /// <summary>
        /// Update an excisiting or create a new assignmetn based on this form
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns></returns>
        public Assignment ToPoco(Assignment assignment = null)
        {
            if (assignment == null)
                assignment = new Assignment();

            assignment.Id = this.Id;
            assignment.Title = this.Title;
            assignment.Description = this.Description;
            assignment.Metadata = this.Metadata;
            assignment.Points = this.Points;

            if(this.Level != null)
                assignment.Level = (AssignmentLevel)Enum.Parse(typeof(AssignmentLevel), this.Level);

            if(this.AssignmentType != null)
                assignment.AssignmentType = (AssignmentType)Enum.Parse(typeof(AssignmentType), this.AssignmentType);

            return assignment;
        }
    }

    public class AssignmentSubmissionFormVM
    {
        public string Token { get; set; }

        public string UserId { get; set; }
    }
}

