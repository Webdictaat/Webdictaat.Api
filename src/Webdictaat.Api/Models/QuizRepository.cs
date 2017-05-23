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
using System.Threading.Tasks;

namespace Webdictaat.CMS.Models
{
    public interface IQuizRepository
    {
        QuizVM CreateQuiz(string dictaatName, QuizVM quiz);
        QuizVM GetQuiz(int quizId, string userId);
        QuizAttemptVM AddAttempt(int quizId, string userId, ICollection<int> givenAnswers);
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
            qa = _context.QuizAttempts.OrderByDescending(mqa => mqa.Timestamp).Include("Answers.Answer").FirstOrDefault(attempt => attempt.Id == qa.Id);

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
                .Include("Questions.Question.Answers.QuizAttempts")          
                .FirstOrDefault(q => q.Id == quizId);

            if (quiz == null)
                return null;
            
            var vm = new QuizVM(quiz);

            if (userId != null)
            {
                vm.MyAttempts = _context.QuizAttempts
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
                        Question = new Question()
                        {
                            Text = qForm.Text,
                            Answers = qForm.Answers.Select(a =>
                                new Answer() { Text = a.Text, IsCorrect = a.IsCorrect }).ToList()
                        }
                    });
                }
            }

            _context.SaveChanges();
            return this.GetQuiz(quiz.Id, null);
        }
    }
}