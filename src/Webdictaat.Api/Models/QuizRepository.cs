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
using Webdictaat.Api.ViewModels;

namespace Webdictaat.CMS.Models
{
    public interface IQuizRepository
    {
        QuizVM CreateQuiz(string dictaatName, QuizVM quiz);
        QuizVM GetQuiz(int quizId, string userId);
        QuizAttemptVM AddAttempt(int quizId, string userId, ICollection<int> givenAnswers);
    }

    public class QuizRepository : IQuizRepository
    {
        private WebdictaatContext _context;

        public QuizRepository(WebdictaatContext context)
        {
            _context = context; 
        }

        public QuizAttemptVM AddAttempt(int quizId, string userId, ICollection<int> givenAnswers)
        {
            QuizAttempt qa = new QuizAttempt()
            {
                QuizId = quizId,
                UserId = userId,
                Answers = givenAnswers.Select(answerId => new QuizAttemptAnswer() { AnswerId = answerId }).ToList(),
                Timestamp = DateTime.Now,
            };

            _context.QuizAttempts.Add(qa);
            _context.SaveChanges();

            //retrieve fresh attempt from database, so we can include the full answer objects
            qa = _context.QuizAttempts.Include("Answers.Answer").FirstOrDefault(attempt => attempt.Id == qa.Id);

            return new QuizAttemptVM(qa);
        }

        public QuizVM CreateQuiz(string dictaatName, QuizVM quiz)
        {
            var q = new Quiz()
            {
                DictaatDetailsName = dictaatName,
                Title = quiz.Title,
                Description = quiz.Description,
                Timestamp = DateTime.Now,
                Questions = quiz.Questions.Select(question => new QuestionQuiz(question.ToPoco())).ToList()
            };

            _context.Quizes.Add(q);
            _context.SaveChanges();

            return new QuizVM(q);
        }

        public QuizVM GetQuiz(int quizId, string userId)
        {
            Quiz quiz = _context.Quizes.Include("Questions.Question.Answers.QuizAttempts")
                .FirstOrDefault(q => q.Id == quizId);

            if (quiz == null)
                return null;
            
            var vm = new QuizVM(quiz);
            vm.MyAttempts = _context.QuizAttempts
                    .Where(qa => qa.UserId == userId && qa.QuizId == quizId )
                    .ToList()
                    .Select(qa => new QuizAttemptVM(qa));


            return vm;
        }
    }
}