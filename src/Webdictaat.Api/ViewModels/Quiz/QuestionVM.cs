using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.CMS.ViewModels
{
    public class QuestionVM
    {
        public bool IsDeleted;

        public int Id { get; set; }
        public string Text { get; set; }

        public IList<AnswerVM> Answers { get; set; }

        public QuestionVM()
        {
            this.Answers = new List<AnswerVM>();
        }

        public QuestionVM(Question question)
        {
            this.Id = question.Id;
            this.Text = question.Text;
            this.IsDeleted = question.IsDeleted;
            this.Answers = question.Answers.Where(a => !a.IsDeleted).Select(a => new AnswerVM(a)).ToList();
        }

        internal Question ToPoco()
        {
            return new Question()
            {
                Id = this.Id,
                Text = this.Text,
                Answers = this.Answers.Select(a => a.ToPoco()).ToList(),
            };
        }
    }
}
