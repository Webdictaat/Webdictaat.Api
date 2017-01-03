using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.CMS.ViewModels
{
    public class AnswerVM
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }


        //oeps lege constructor vergeten.
        public AnswerVM()
        {

        }

        public AnswerVM(Answer answer)
        {
            this.Text = answer.Text;
            this.IsCorrect = answer.IsCorrect;
        }
    }
}
