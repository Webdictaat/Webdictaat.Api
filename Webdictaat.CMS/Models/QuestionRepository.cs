using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using Webdictaat.Core;
using Microsoft.Extensions.Options;
using Webdictaat.CMS.ViewModels;
using Webdictaat.Domain;
using Microsoft.EntityFrameworkCore;

namespace Webdictaat.CMS.Models
{
    public interface IQuestionRepository
    {
        QuestionVM CreateQuestion(QuestionVM question);
        QuestionVM GetQuestion(int questionId);
    }

    public class QuestionRepository : IQuestionRepository
    {
        private DomainContext _context;

        public QuestionRepository(DomainContext context)
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
    }
}