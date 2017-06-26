using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Webdictaat.Domain;
using Webdictaat.Domain.Assignments;
using Webdictaat.Domain.User;

namespace Webdictaat.Data
{
    public class WebdictaatContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<AssignmentSubmission> AssignmentSubmissions { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<DictaatDetails> DictaatDetails { get; set; }

        public DbSet<Achievement> Achievements { get; set; }
        
        public DbSet<DictaatAchievement> DictaatAchievements { get; set; }

        public DbSet<UserAchievement> UserAchievements { get; set; }

        /** Quizes **/

        public DbSet<Quiz> Quizes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuizAttempt> QuizAttempts { get; set; }

        public DbSet<DictaatSession> DictaatSession { get; set; }

        public WebdictaatContext(DbContextOptions<WebdictaatContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            //koppel tabellen
            builder.Entity<QuestionQuiz>().HasKey(t => new { t.QuestionId, t.QuizId });
            builder.Entity<QuizAttemptAnswer>().HasKey(t => new { t.QuizAttemptId, t.AnswerId });
            builder.Entity<AssignmentSubmission>().HasKey(t => new { t.AssignmentId, t.UserId });
            builder.Entity<DictaatContributer>().HasKey(t => new { t.UserId, t.DictaatDetailsId });
            builder.Entity<DictaatAchievement>().HasKey(t => new { t.DictaatName, t.AchievementId });
            builder.Entity<UserAchievement>().HasKey(t => new { t.UserId, t.AchievementId });
            builder.Entity<DictaatSessionUser>().HasKey(t => new { t.UserId, t.DictaatSessionId });
        }

    }
}
