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

        public string Type { get; set; }

        /// <summary>
        /// Json based string
        /// </summary>
        public dynamic Body { get; set; }


        public QuestionVM()
        {
           
        }

        public QuestionVM(Question question)
        {
            this.Id = question.Id;
            this.Text = question.Text;
            this.Type = question.QuestionType;


            //check if question contains old scool answers
            if (question.Body == null)
            {
                this.Body = new
                {
                    answers = question.Answers.Select(a => new { text = a.Text, isCorrect = a.IsCorrect })
                };
                this.Type = "mc";
            }
            else
            {
                this.Body = Newtonsoft.Json.Linq.JObject.Parse(question.Body);

            }

            this.IsDeleted = question.IsDeleted;
        }

        internal Question ToPoco()
        {
            return new Question()
            {
                Id = this.Id,
                Text = this.Text,
                QuestionType = this.Type,
                Body = Newtonsoft.Json.JsonConvert.SerializeObject(this.Body)
            };
        }
    }
}
