using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using Webdictaat.Core;
using Microsoft.Extensions.Options;
using Webdictaat.CMS.ViewModels;
using Webdictaat.Domain;
using Microsoft.EntityFrameworkCore;
using Webdictaat.Data;

namespace Webdictaat.CMS.Models
{
    public interface IQuestionRepository
    {
        QuestionVM CreateQuestion(QuestionVM question);
        QuestionVM GetQuestion(int questionId);
        QuestionVM DeleteQuestion(int questionId);
        QuestionVM UpdateQuestion(QuestionVM question);
    }

    public class QuestionRepository : IQuestionRepository
    {
        private WebdictaatContext _context;

        public QuestionRepository(WebdictaatContext context)
        {
            _context = context; 
        }

        public QuestionVM CreateQuestion(QuestionVM question)
        {
            var q = new Question()
            {
                Text = question.Text,
                Answers = question.Answers.Select(a =>
                    new Answer() { Text = a.Text, IsCorrect = a.IsCorrect }).ToList()
            };

            _context.Questions.Add(q);
            _context.SaveChanges();
            question.Id = q.Id;
            return question;
                
        }

        public QuestionVM DeleteQuestion(int questionId)
        {
            Question question = _context.Questions.FirstOrDefault(q => q.Id == questionId);

            if (question == null)
                return null;

            question.IsDeleted = true;
            _context.SaveChanges();

            return new QuestionVM(question);
        }

        /// <summary>
        /// Get a question with a specific unique Id. 
        /// If there is no question with the specific id, return null.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public QuestionVM GetQuestion(int questionId)
        {
            Question question = _context.Questions.Include(q => q.Answers).FirstOrDefault(q => q.Id == questionId);

            if (question == null)
                return null;

            return new QuestionVM(question);
        }

        public QuestionVM UpdateQuestion(QuestionVM form)
        {
            Question question = _context.Questions.Include(q => q.Answers).FirstOrDefault(q => q.Id == form.Id);

            if (question == null)
                return null;

            question.Text = form.Text;
           
            //eerst zoeken naar bestaande answers
            foreach(var answer in question.Answers)
            {
                var answerForm = form.Answers.FirstOrDefault(a => a.Id == answer.Id);

                if (answerForm != null){ //mag blijven
                    answer.IsCorrect = answerForm.IsCorrect;
                    form.Answers.Remove(answerForm); // al gehad
                }
                else
                {
                    answer.IsDeleted = true;
                }
            }

            //overgebleven answers zijn nieuw
            foreach (var answer in form.Answers)
            {
                question.Answers.Add(new Answer()
                {
                    IsCorrect = answer.IsCorrect,
                    Text = answer.Text
                });
            }

            //klaar
            _context.SaveChanges();
            return new QuestionVM(question);
        }
    }
}