using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webdictaat.Api.ViewModels
{
    public class QuizAttemptForm
    {
        public int QuizId { get; set; }

        public ICollection<int> GivenAnswers { get; set; } 
    }
}
