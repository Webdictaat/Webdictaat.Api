using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;

namespace Webdictaat.Api.ViewModels
{
    public class QuestionVM
    {
        public bool IsDeleted;

        public int Id { get; set; }
        public string Text { get; set; }

        /// <summary>
        /// Json based string
        /// </summary>
        public string Body { get; set; }


        public QuestionVM()
        {
           
        }

        public QuestionVM(Question question)
        {
            this.Id = question.Id;
            this.Text = question.Text;
            this.IsDeleted = question.IsDeleted;
        }

        internal Question ToPoco()
        {
            return new Question()
            {
                Id = this.Id,
                Text = this.Text,
                Body = this.Body
            };
        }
    }
}
