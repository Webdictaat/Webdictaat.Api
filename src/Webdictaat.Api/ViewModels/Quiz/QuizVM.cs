using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.CMS.ViewModels;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class QuizVM
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<QuestionVM> Questions { get; set; }
        public IEnumerable<QuizAttemptVM> MyAttempts { get; set; }

        public QuizVM()
        {
            this.Questions = new List<QuestionVM>();
        }

        public QuizVM(Quiz quiz)
        {
            this.Id = quiz.Id;
            this.Title = quiz.Title;
            this.Description = quiz.Description;
            this.Questions = quiz.Questions.Where(q => !q.Question.IsDeleted).Select(q => new QuestionVM(q.Question));
        }

     

    }
}
