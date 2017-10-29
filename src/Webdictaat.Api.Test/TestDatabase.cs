using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Data;
using Webdictaat.Domain;

namespace Webdictaat.Api.Test
{
    public static class TestDatabase
    {
        public static void CreateQuizes(WebdictaatContext context)
        {

            context.Quizes.Add(new Quiz() //Quiz 1
            {
                Id = 11,
                Title = "Title",
                Description = "Desc",
                Questions = new List<QuestionQuiz>()
                {
                    new QuestionQuiz() { QuestionId = 11},
                    new QuestionQuiz() { QuestionId = 12}
                }
            });

            context.SaveChanges();

        }

        public static void CreateQuestions(WebdictaatContext context)
        {

        }
    }
}
