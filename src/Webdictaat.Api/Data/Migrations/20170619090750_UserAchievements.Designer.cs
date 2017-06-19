using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Webdictaat.Data;

namespace Webdictaat.Api.Migrations
{
    [DbContext(typeof(WebdictaatContext))]
    [Migration("20170619090750_UserAchievements")]
    partial class UserAchievements
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Webdictaat.Domain.Achievement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DictaatName");

                    b.Property<bool>("Hidden");

                    b.Property<string>("Image")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("Webdictaat.Domain.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsCorrect");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("QuestionId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("Webdictaat.Domain.Assignments.Assignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AssignmentSecret");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("DictaatDetailsId")
                        .IsRequired();

                    b.Property<int>("Level");

                    b.Property<string>("Metadata");

                    b.Property<int>("Points");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DictaatDetailsId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("Webdictaat.Domain.Assignments.AssignmentSubmission", b =>
                {
                    b.Property<int>("AssignmentId");

                    b.Property<string>("UserId");

                    b.Property<int>("PointsRecieved");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("AssignmentId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("AssignmentSubmissions");
                });

            modelBuilder.Entity("Webdictaat.Domain.DictaatAchievement", b =>
                {
                    b.Property<string>("DictaatName");

                    b.Property<int>("AchievementId");

                    b.Property<string>("GroupName")
                        .IsRequired();

                    b.Property<int>("GroupOrder");

                    b.HasKey("DictaatName", "AchievementId");

                    b.HasIndex("AchievementId");

                    b.ToTable("DictaatAchievements");
                });

            modelBuilder.Entity("Webdictaat.Domain.DictaatContributer", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("DictaatDetailsId");

                    b.HasKey("UserId", "DictaatDetailsId");

                    b.HasIndex("DictaatDetailsId");

                    b.ToTable("DictaatContributer");
                });

            modelBuilder.Entity("Webdictaat.Domain.DictaatDetails", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DictaatOwnerId")
                        .IsRequired();

                    b.HasKey("Name");

                    b.HasIndex("DictaatOwnerId");

                    b.ToTable("DictaatDetails");
                });

            modelBuilder.Entity("Webdictaat.Domain.DictaatSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DictaatDetailsId");

                    b.Property<DateTime?>("EndedOn");

                    b.Property<DateTime?>("StartedOn");

                    b.HasKey("Id");

                    b.HasIndex("DictaatDetailsId");

                    b.ToTable("DictaatSession");
                });

            modelBuilder.Entity("Webdictaat.Domain.DictaatSessionUser", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("DictaatSessionId");

                    b.HasKey("UserId", "DictaatSessionId");

                    b.HasIndex("DictaatSessionId");

                    b.ToTable("DictaatSessionUser");
                });

            modelBuilder.Entity("Webdictaat.Domain.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Webdictaat.Domain.QuestionQuiz", b =>
                {
                    b.Property<int>("QuestionId");

                    b.Property<int>("QuizId");

                    b.HasKey("QuestionId", "QuizId");

                    b.HasIndex("QuizId");

                    b.ToTable("QuestionQuiz");
                });

            modelBuilder.Entity("Webdictaat.Domain.Quiz", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("DictaatDetailsName")
                        .IsRequired();

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Quizes");
                });

            modelBuilder.Entity("Webdictaat.Domain.QuizAttempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuizId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("QuizAttempts");
                });

            modelBuilder.Entity("Webdictaat.Domain.QuizAttemptAnswer", b =>
                {
                    b.Property<int>("QuizAttemptId");

                    b.Property<int>("AnswerId");

                    b.HasKey("QuizAttemptId", "AnswerId");

                    b.HasIndex("AnswerId");

                    b.ToTable("QuizAttemptAnswer");
                });

            modelBuilder.Entity("Webdictaat.Domain.Rate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Emotion");

                    b.Property<string>("Feedback")
                        .IsRequired();

                    b.Property<int>("RatingId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RatingId");

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("Webdictaat.Domain.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("DictaatDetailsName")
                        .IsRequired();

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Webdictaat.Domain.User.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Webdictaat.Domain.UserAchievement", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("AchievementId");

                    b.Property<bool>("Completed");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("UserId", "AchievementId");

                    b.HasIndex("AchievementId");

                    b.ToTable("UserAchievement");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Webdictaat.Domain.User.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Webdictaat.Domain.User.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webdictaat.Domain.User.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.Answer", b =>
                {
                    b.HasOne("Webdictaat.Domain.Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.Assignments.Assignment", b =>
                {
                    b.HasOne("Webdictaat.Domain.DictaatDetails", "DictaatDetails")
                        .WithMany("Assignments")
                        .HasForeignKey("DictaatDetailsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.Assignments.AssignmentSubmission", b =>
                {
                    b.HasOne("Webdictaat.Domain.Assignments.Assignment")
                        .WithMany("Attempts")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webdictaat.Domain.User.ApplicationUser", "User")
                        .WithMany("AssignmentSubmissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.DictaatAchievement", b =>
                {
                    b.HasOne("Webdictaat.Domain.Achievement", "Achievement")
                        .WithMany()
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webdictaat.Domain.DictaatDetails", "Dictaat")
                        .WithMany("Achievements")
                        .HasForeignKey("DictaatName")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.DictaatContributer", b =>
                {
                    b.HasOne("Webdictaat.Domain.DictaatDetails", "DictaatDetails")
                        .WithMany("Contributers")
                        .HasForeignKey("DictaatDetailsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webdictaat.Domain.User.ApplicationUser", "User")
                        .WithMany("ContributedDictaten")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.DictaatDetails", b =>
                {
                    b.HasOne("Webdictaat.Domain.User.ApplicationUser", "DictaatOwner")
                        .WithMany("OwnedDictaten")
                        .HasForeignKey("DictaatOwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.DictaatSession", b =>
                {
                    b.HasOne("Webdictaat.Domain.DictaatDetails", "DictaatDetails")
                        .WithMany("Sessions")
                        .HasForeignKey("DictaatDetailsId");
                });

            modelBuilder.Entity("Webdictaat.Domain.DictaatSessionUser", b =>
                {
                    b.HasOne("Webdictaat.Domain.DictaatSession", "DictaatSession")
                        .WithMany("Participants")
                        .HasForeignKey("DictaatSessionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webdictaat.Domain.User.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.QuestionQuiz", b =>
                {
                    b.HasOne("Webdictaat.Domain.Question", "Question")
                        .WithMany("Quizes")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webdictaat.Domain.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.QuizAttempt", b =>
                {
                    b.HasOne("Webdictaat.Domain.Quiz")
                        .WithMany("QuizAttempts")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.QuizAttemptAnswer", b =>
                {
                    b.HasOne("Webdictaat.Domain.Answer", "Answer")
                        .WithMany("QuizAttempts")
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webdictaat.Domain.QuizAttempt", "QuizAttempt")
                        .WithMany("Answers")
                        .HasForeignKey("QuizAttemptId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.Rate", b =>
                {
                    b.HasOne("Webdictaat.Domain.Rating", "Rating")
                        .WithMany("Rates")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Webdictaat.Domain.UserAchievement", b =>
                {
                    b.HasOne("Webdictaat.Domain.Achievement", "Achievement")
                        .WithMany()
                        .HasForeignKey("AchievementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Webdictaat.Domain.User.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
