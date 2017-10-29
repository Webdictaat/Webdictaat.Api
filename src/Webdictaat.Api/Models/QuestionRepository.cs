using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Collections.Generic;
using Webdictaat.Core;
using Microsoft.Extensions.Options;
using Webdictaat.Api.ViewModels;
using Webdictaat.Domain;
using Microsoft.EntityFrameworkCore;
using Webdictaat.Data;

namespace Webdictaat.Api.Models
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
            _context.Questions.Add(question.ToPoco());
            _context.SaveChanges();
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
            Question question = _context.Questions.FirstOrDefault(q => q.Id == questionId);

            if (question == null)
                return null;

            return new QuestionVM(question);
        }

        public QuestionVM UpdateQuestion(QuestionVM form)
        {
            Question question = form.ToPoco();
            _context.Questions.Attach(question);
            _context.SaveChanges();
            return new QuestionVM(question);
        }
    }
}