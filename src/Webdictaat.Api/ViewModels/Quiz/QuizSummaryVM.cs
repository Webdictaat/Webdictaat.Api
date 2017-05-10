using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class QuizSummaryVM
    {
        public QuizSummaryVM()
        {

        }

        public QuizSummaryVM(Quiz q)
        {
            this.Id = q.Id;
            this.Title = q.Title;
            this.Description = q.Description;

            this.QuestionCount = q.Questions.Count();
            this.CompletedByCount = q.QuizAttempts.GroupBy(qa => qa.UserId).Count();

        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int QuestionCount {get; set; }

        public int CompletedByCount { get; set; }

    }

}
