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
        QuizVM AddAttempt(int quizId, string userId, IEnumerable<QuestionAttemptVM> attempt);
        ICollection<QuizVM> GetQuizes(string dictaatName, string userId);
        QuizVM UpdateQuiz(string dictaatName, QuizVM quiz);
    }

    public class QuizRepository : IQuizRepository
    {
        private WebdictaatContext _context;
        private IQuestionRepository _questionRepo;
        private IAssignmentRepository _assignmentRepo;

        public QuizRepository(IQuestionRepository questionRepo,
            IAssignmentRepository assignmentRepo, WebdictaatContext context)
        {
            _context = context;
            _questionRepo = questionRepo;
            _assignmentRepo = assignmentRepo;
        }

        public QuizVM AddAttempt(int quizId, string userId, IEnumerable<QuestionAttemptVM> attempt)
        {
            var quiz = _context.Quizes.FirstOrDefault(q => q.Id == quizId);

            QuizAttempt qa = new QuizAttempt()
            {
                QuizId = quizId,
                UserId = userId,
                Timestamp = DateTime.Now,
                QuestionsAnswered = attempt.ToList().Select(a => a.ToPoco()).ToList()
            };

            //if all questions are correct, complete assignment if available
            if(!qa.QuestionsAnswered.Any(q => !q.IsCorrect) && quiz.AssignmentId.HasValue)
            {
                _assignmentRepo.CompleteAssignment(quiz.AssignmentId.Value, userId, true);
            }

            _context.QuizAttempts.Add(qa);
            _context.SaveChanges();

            return this.GetQuiz(quizId, userId);
        }

        public ICollection<QuizVM> GetQuizes(string dictaatId, string userId)
        {
            var quizes = _context.Quizes
                .Include("Questions.Question.Answers")
                .Include("Assignment.Attempts")
                .Where(q => q.DictaatDetailsName == dictaatId || q.Assignment.DictaatDetailsId == dictaatId).ToList();

            return quizes.Select(q => new QuizVM(q)).ToList();
        }

        public QuizVM CreateQuiz(string dictaatName, QuizVM quiz)
        {
            var q = new Quiz()
            {
                DictaatDetailsName = dictaatName,
                Timestamp = DateTime.Now,
                Shuffle = quiz.Shuffle,
                Questions = quiz.Questions.Select(question => new QuestionQuiz(question.ToPoco())).ToList(),
                Assignment = quiz.Assignment.ToPoco()
            };

            q.Assignment.AssignmentType = Domain.Assignments.AssignmentType.Quiz;

            q.Assignment.DictaatDetailsId = dictaatName;

            _context.Quizes.Add(q);
            _context.SaveChanges();

            return new QuizVM(q);
        }

        public QuizVM GetQuiz(int quizId, string userId = null)
        {
            Quiz quiz = _context.Quizes
                .Include("Questions.Question.Answers")          
                .Include("Assignment.Attempts")
                .FirstOrDefault(q => q.Id == quizId);

            if (quiz == null)
                return null;

            //update the quiz to have an assignment
            //this code can be removed on an update where quiz title and desc are removed
            if(quiz.Assignment == null)
            {
                quiz.Assignment = new Domain.Assignments.Assignment()
                {
                    Title = quiz.Title,
                    Description = quiz.Description,
                    AssignmentType = Domain.Assignments.AssignmentType.Quiz,
                    Metadata = "1.1",
                    Points = 0,
                    Level = Domain.Assignments.AssignmentLevel.Bronze,
                    DictaatDetailsId = quiz.DictaatDetailsName
                };
                _context.SaveChanges();
            }
            
            var vm = new QuizVM(quiz);

            if (userId != null)
            {
                vm.MyAttempts = _context.QuizAttempts
                    .Include(q => q.QuestionsAnswered)
                    .Where(qa => qa.UserId == userId && qa.QuizId == quizId)
                    .OrderByDescending(qa => qa.Timestamp)
                    .ToList()
                    .Select(qa => new QuizAttemptVM(qa));

                vm.IsComplete = _context.AssignmentSubmissions
                    .Any(a => a.AssignmentId == quiz.AssignmentId &&
                    a.UserId == userId);
            }


            return vm;
        }

        public QuizVM UpdateQuiz(string dictaatName, QuizVM form)
        {
            Quiz quiz = _context.Quizes
                .Include("Questions")
                .Include("Assignment")
                .FirstOrDefault(q => q.Id == form.Id);

            quiz.Assignment = form.Assignment.ToPoco(quiz.Assignment);

            //update all questions and add all new
            foreach (var qForm in form.Questions)
            {
                if(qForm.Id != 0)
                {
                    //update
                    _questionRepo.UpdateQuestion(qForm);
                }
                else
                {
                    //add
                    quiz.Questions.Add(new QuestionQuiz()
                    {
                        Question = qForm.ToPoco()
                    });
                } 
            }

            //remove all questions that are not in the form
            var questions = quiz.Questions; //copy the list
            quiz.Questions.ToList().ForEach(q =>
            {
                if(!form.Questions.Any(qform => qform.Id == q.QuestionId))
                {
                    questions.Remove(q); //remove from the copied list
                }
            });
            quiz.Questions = questions;//set the new list

            _context.SaveChanges();
            return this.GetQuiz(quiz.Id, null);
        }
    }
}