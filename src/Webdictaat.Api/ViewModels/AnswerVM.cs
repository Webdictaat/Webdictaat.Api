using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.CMS.ViewModels
{
    public class AnswerVM
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsCorrect { get; set; }

        public int AnsweredCount { get; set; }

        public bool IsDeleted { get; set; }


        //oeps lege constructor vergeten.
        public AnswerVM()
        {

        }

        public AnswerVM(Answer answer)
        {
            this.Id = answer.Id;
            this.Text = answer.Text;
            this.IsCorrect = answer.IsCorrect;
            this.IsDeleted = answer.IsDeleted;
            this.AnsweredCount = answer.QuizAttempts.Count();
        }

        internal Answer ToPoco()
        {
            return new Answer()
            {
                Id = this.Id,
                Text = this.Text,
                IsCorrect = this.IsCorrect,
            };
        }
    }
}
