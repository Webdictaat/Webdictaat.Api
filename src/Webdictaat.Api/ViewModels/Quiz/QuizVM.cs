using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Api.ViewModels;
using Webdictaat.Api.ViewModels.Assignments;
using Webdictaat.Domain;
using Webdictaat.Domain.Assignments;

namespace Webdictaat.Api.ViewModels
{
    public class QuizVM
    {
        public int Id { get; set; }

        /// <summary>
        /// Related assignment
        /// </summary>
        public AssignmentFormVM Assignment { get; set; }

        /// <summary>
        /// Indicated that the question order should be random
        /// </summary>
        public bool Shuffle { get; internal set; }

        public IEnumerable<QuestionVM> Questions { get; set; }
        public IEnumerable<QuizAttemptVM> MyAttempts { get; set; }
        /// <summary>
        /// A boolean to indicate to a user that he or she completed this quiz
        /// </summary>
        public bool IsComplete { get; set; }
        /// <summary>
        /// a boolean to indicate to a contributer that this quiz is still in the old format
        /// </summary>
        public bool IsOutdated { get; set; }

        public QuizVM()
        {
            this.Questions = new List<QuestionVM>();
        }

        public QuizVM(Quiz quiz)
        {
            //Quiz items
            this.Id = quiz.Id;
            this.Questions = quiz.Questions.Where(q => !q.Question.IsDeleted).Select(q => new QuestionVM(q.Question));
            this.Shuffle = quiz.Shuffle;

            if (quiz.Assignment != null)
            {
                this.Assignment = new AssignmentFormVM(quiz.Assignment);
            }
            else
            {
                IsOutdated = true;
                this.Assignment = new AssignmentFormVM()
                {
                    Title = quiz.Title
                };
            }
            
        }

    }
}
