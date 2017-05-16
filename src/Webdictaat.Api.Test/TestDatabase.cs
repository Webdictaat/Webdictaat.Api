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
            context.Questions.Add(new Question() //Question 1
            {
                Id = 11,
                Text = "Q1",
                Answers = new List<Answer>()
                {
                    new Answer() { Id = 11, Text = "Q1-A1" },
                    new Answer() { Id = 12, Text = "Q1-A2", IsCorrect = true },
                }
            });

            context.Questions.Add(new Question() //Question 2
            {
                Id = 12,
                Text = "Q2",
                Answers = new List<Answer>()
                {
                    new Answer() { Id = 13, Text = "Q2-A1" },
                    new Answer() { Id = 14, Text = "Q2-A2", IsCorrect = true },
                }
            });

            context.SaveChanges();
        }
    }
}
