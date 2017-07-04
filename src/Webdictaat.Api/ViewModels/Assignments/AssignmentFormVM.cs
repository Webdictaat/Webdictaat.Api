using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Api.ViewModels.Assignments
{
    public class AssignmentFormVM
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Metadata { get; set; }

        public int Points { get; set; }

        public string Level { get; set; }
    }

    public class AssignmentSubmissionFormVM
    {
        public string Token { get; set; }

        public string UserId { get; set; }
    }
}
