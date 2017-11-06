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
using System.Threading.Tasks;

namespace Webdictaat.Api.Models
{
    public interface IQuizRepository
    {
        QuizVM CreateQuiz(string dictaatName, QuizVM quiz);
        QuizVM GetQuiz(int quizId, string userId);
        QuizAttemptVM AddAttempt(int quizId, string userId, IEnumerable<QuestionAttemptVM> attempt);
        ICollection<QuizSummaryVM> GetQuizes(string dictaatName, string userId);
        QuizVM UpdateQuiz(string dictaatName, QuizVM quiz);
    }

    public class QuizRepository : IQuizRepository
    {
        private WebdictaatContext _context;
        private IQuestionRepository _questionRepo;

        public QuizRepository(IQuestionRepository questionRepo, WebdictaatContext context)
        {
            _context = context;
            _questionRepo = questionRepo;
        }

        public QuizAttemptVM AddAttempt(int quizId, string userId, IEnumerable<QuestionAttemptVM> attempt)
        {
            QuizAttempt qa = new QuizAttempt()
            {
                QuizId = quizId,
                UserId = userId,
                Timestamp = DateTime.Now,
                QuestionsAnswered = attempt.ToList().Select(a => a.ToPoco()).ToList()
            };

            _context.QuizAttempts.Add(qa);
            _context.SaveChanges();

            return new QuizAttemptVM(qa);
        }

        public ICollection<QuizSummaryVM> GetQuizes(string dictaatId, string userId)
        {
            var quizes = _context.Quizes
                .Include("QuizAttempts")
                .Include("Questions")
                .Where(q => q.DictaatDetailsName == dictaatId).ToList();

            return quizes.Select(q => new QuizSummaryVM(q)).ToList();
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

        public QuizVM GetQuiz(int quizId, string userId = null)
        {
            Quiz quiz = _context.Quizes
                .Include("Questions.Question")          
                .FirstOrDefault(q => q.Id == quizId);

            if (quiz == null)
                return null;
            
            var vm = new QuizVM(quiz);

            if (userId != null)
            {
                vm.MyAttempts = _context.QuizAttempts
                        .Include(q => q.QuestionsAnswered)
                        .Where(qa => qa.UserId == userId && qa.QuizId == quizId)
                        .OrderByDescending(qa => qa.Timestamp)
                        .ToList()
                        .Select(qa => new QuizAttemptVM(qa));
            }


            return vm;
        }

        public QuizVM UpdateQuiz(string dictaatName, QuizVM form)
        {
            Quiz quiz = _context.Quizes.Include("Questions").FirstOrDefault(q => q.Id == form.Id);
            quiz.Title = form.Title;
            quiz.Description = form.Description;

            //update all questions and add all new
            foreach(var qForm in form.Questions)
            {
                if(qForm.Id != 0)
                {
                    _questionRepo.UpdateQuestion(qForm);
                }
                else
                {
                    quiz.Questions.Add(new QuestionQuiz()
                    {
                        Question = qForm.ToPoco()
                    });
                }
            }

            _context.SaveChanges();
            return this.GetQuiz(quiz.Id, null);
        }
    }
}