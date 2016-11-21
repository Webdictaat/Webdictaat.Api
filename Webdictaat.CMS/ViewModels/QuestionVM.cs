using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.CMS.ViewModels
{
    public class QuestionVM
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public IList<AnswerVM> Answers { get; set; }

        public QuestionVM()
        {
            Answers = new List<AnswerVM>();
        }

        public QuestionVM(Question question)
        {
            this.Id = question.Id;
            this.Text = question.Text;
            this.Answers = question.Answers.Select(a => new AnswerVM(a)).ToList();
        }
    }
}
