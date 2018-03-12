/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/

/** Before running this script drop and create the database webdictaat.test **/

USE [Webdictaat.Test]
GO
ALTER TABLE [dbo].[UserAchievements] DROP CONSTRAINT [FK_UserAchievements_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[UserAchievements] DROP CONSTRAINT [FK_UserAchievements_Achievements_AchievementId]
GO
ALTER TABLE [dbo].[Rates] DROP CONSTRAINT [FK_Rates_Ratings_RatingId]
GO
ALTER TABLE [dbo].[Quizes] DROP CONSTRAINT [FK_Quizes_Assignments_AssignmentId]
GO
ALTER TABLE [dbo].[QuizAttempts] DROP CONSTRAINT [FK_QuizAttempts_Quizes_QuizId]
GO
ALTER TABLE [dbo].[QuizAttemptQuestion] DROP CONSTRAINT [FK_QuizAttemptQuestion_QuizAttempts_QuizAttemptId]
GO
ALTER TABLE [dbo].[QuizAttemptQuestion] DROP CONSTRAINT [FK_QuizAttemptQuestion_Questions_QuestionId]
GO
ALTER TABLE [dbo].[QuestionQuiz] DROP CONSTRAINT [FK_QuestionQuiz_Quizes_QuizId]
GO
ALTER TABLE [dbo].[QuestionQuiz] DROP CONSTRAINT [FK_QuestionQuiz_Questions_QuestionId]
GO
ALTER TABLE [dbo].[PollVotes] DROP CONSTRAINT [FK_PollVotes_PollOption_PollOptionId]
GO
ALTER TABLE [dbo].[PollOption] DROP CONSTRAINT [FK_PollOption_Polls_PollId]
GO
ALTER TABLE [dbo].[DictaatSessionUser] DROP CONSTRAINT [FK_DictaatSessionUser_DictaatSession_DictaatSessionId]
GO
ALTER TABLE [dbo].[DictaatSessionUser] DROP CONSTRAINT [FK_DictaatSessionUser_DictaatGroup_Group_DictaatName]
GO
ALTER TABLE [dbo].[DictaatSessionUser] DROP CONSTRAINT [FK_DictaatSessionUser_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[DictaatSession] DROP CONSTRAINT [FK_DictaatSession_DictaatDetails_DictaatDetailsId]
GO
ALTER TABLE [dbo].[DictaatGroup] DROP CONSTRAINT [FK_DictaatGroup_DictaatDetails_DictaatName]
GO
ALTER TABLE [dbo].[DictaatDetails] DROP CONSTRAINT [FK_DictaatDetails_AspNetUsers_DictaatOwnerId]
GO
ALTER TABLE [dbo].[DictaatContributer] DROP CONSTRAINT [FK_DictaatContributer_DictaatDetails_DictaatDetailsId]
GO
ALTER TABLE [dbo].[DictaatContributer] DROP CONSTRAINT [FK_DictaatContributer_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[DictaatAchievements] DROP CONSTRAINT [FK_DictaatAchievements_DictaatDetails_DictaatName]
GO
ALTER TABLE [dbo].[DictaatAchievements] DROP CONSTRAINT [FK_DictaatAchievements_Achievements_AchievementId]
GO
ALTER TABLE [dbo].[AssignmentSubmissions] DROP CONSTRAINT [FK_AssignmentSubmissions_Assignments_AssignmentId]
GO
ALTER TABLE [dbo].[Assignments] DROP CONSTRAINT [FK_Assignments_DictaatDetails_DictaatDetailsId]
GO
ALTER TABLE [dbo].[AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[Answer] DROP CONSTRAINT [FK_Answer_Questions_QuestionId]
GO
ALTER TABLE [dbo].[Ratings] DROP CONSTRAINT [DF__Ratings__Timesta__5FB337D6]
GO
ALTER TABLE [dbo].[Ratings] DROP CONSTRAINT [DF__Ratings__Dictaat__5EBF139D]
GO
ALTER TABLE [dbo].[Quizes] DROP CONSTRAINT [DF__Quizes__Shuffle__5DCAEF64]
GO
ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [DF__Questions__IsDel__5CD6CB2B]
GO
ALTER TABLE [dbo].[DictaatDetails] DROP CONSTRAINT [DF__DictaatDe__IsEna__5BE2A6F2]
GO
ALTER TABLE [dbo].[AssignmentSubmissions] DROP CONSTRAINT [DF__Assignmen__Accep__5AEE82B9]
GO
ALTER TABLE [dbo].[Assignments] DROP CONSTRAINT [DF__Assignmen__Level__59FA5E80]
GO
ALTER TABLE [dbo].[Answer] DROP CONSTRAINT [DF__Answer__IsDelete__59063A47]
GO
/****** Object:  Index [IX_UserAchievements_AchievementId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_UserAchievements_AchievementId] ON [dbo].[UserAchievements]
GO
/****** Object:  Index [IX_Rates_RatingId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_Rates_RatingId] ON [dbo].[Rates]
GO
/****** Object:  Index [IX_Quizes_AssignmentId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_Quizes_AssignmentId] ON [dbo].[Quizes]
GO
/****** Object:  Index [IX_QuizAttempts_QuizId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_QuizAttempts_QuizId] ON [dbo].[QuizAttempts]
GO
/****** Object:  Index [IX_QuizAttemptQuestion_QuestionId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_QuizAttemptQuestion_QuestionId] ON [dbo].[QuizAttemptQuestion]
GO
/****** Object:  Index [IX_QuestionQuiz_QuizId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_QuestionQuiz_QuizId] ON [dbo].[QuestionQuiz]
GO
/****** Object:  Index [IX_PollVotes_PollOptionId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_PollVotes_PollOptionId] ON [dbo].[PollVotes]
GO
/****** Object:  Index [IX_PollOption_PollId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_PollOption_PollId] ON [dbo].[PollOption]
GO
/****** Object:  Index [IX_DictaatSessionUser_Group_DictaatName]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_DictaatSessionUser_Group_DictaatName] ON [dbo].[DictaatSessionUser]
GO
/****** Object:  Index [IX_DictaatSessionUser_DictaatSessionId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_DictaatSessionUser_DictaatSessionId] ON [dbo].[DictaatSessionUser]
GO
/****** Object:  Index [IX_DictaatSession_DictaatDetailsId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_DictaatSession_DictaatDetailsId] ON [dbo].[DictaatSession]
GO
/****** Object:  Index [IX_DictaatGroup_DictaatName]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_DictaatGroup_DictaatName] ON [dbo].[DictaatGroup]
GO
/****** Object:  Index [IX_DictaatDetails_DictaatOwnerId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_DictaatDetails_DictaatOwnerId] ON [dbo].[DictaatDetails]
GO
/****** Object:  Index [IX_DictaatContributer_DictaatDetailsId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_DictaatContributer_DictaatDetailsId] ON [dbo].[DictaatContributer]
GO
/****** Object:  Index [IX_DictaatAchievements_AchievementId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_DictaatAchievements_AchievementId] ON [dbo].[DictaatAchievements]
GO
/****** Object:  Index [IX_Assignments_DictaatDetailsId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_Assignments_DictaatDetailsId] ON [dbo].[Assignments]
GO
/****** Object:  Index [UserNameIndex]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
GO
/****** Object:  Index [EmailIndex]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [EmailIndex] ON [dbo].[AspNetUsers]
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
GO
/****** Object:  Index [IX_Answer_QuestionId]    Script Date: 12-3-2018 17:01:15 ******/
DROP INDEX [IX_Answer_QuestionId] ON [dbo].[Answer]
GO
/****** Object:  Table [dbo].[UserAchievements]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[UserAchievements]
GO
/****** Object:  Table [dbo].[Ratings]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[Ratings]
GO
/****** Object:  Table [dbo].[Rates]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[Rates]
GO
/****** Object:  Table [dbo].[Quizes]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[Quizes]
GO
/****** Object:  Table [dbo].[QuizAttempts]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[QuizAttempts]
GO
/****** Object:  Table [dbo].[QuizAttemptQuestion]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[QuizAttemptQuestion]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[Questions]
GO
/****** Object:  Table [dbo].[QuestionQuiz]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[QuestionQuiz]
GO
/****** Object:  Table [dbo].[PollVotes]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[PollVotes]
GO
/****** Object:  Table [dbo].[Polls]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[Polls]
GO
/****** Object:  Table [dbo].[PollOption]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[PollOption]
GO
/****** Object:  Table [dbo].[DictaatSessionUser]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[DictaatSessionUser]
GO
/****** Object:  Table [dbo].[DictaatSession]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[DictaatSession]
GO
/****** Object:  Table [dbo].[DictaatGroup]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[DictaatGroup]
GO
/****** Object:  Table [dbo].[DictaatDetails]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[DictaatDetails]
GO
/****** Object:  Table [dbo].[DictaatContributer]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[DictaatContributer]
GO
/****** Object:  Table [dbo].[DictaatAchievements]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[DictaatAchievements]
GO
/****** Object:  Table [dbo].[AssignmentSubmissions]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[AssignmentSubmissions]
GO
/****** Object:  Table [dbo].[Assignments]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[Assignments]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[AspNetUserTokens]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[AspNetRoleClaims]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[Answer]
GO
/****** Object:  Table [dbo].[Achievements]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[Achievements]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12-3-2018 17:01:15 ******/
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
USE [master]
GO
/****** Object:  Database [Webdictaat.Test]    Script Date: 12-3-2018 17:01:15 ******/
DROP DATABASE [Webdictaat.Test]
GO
/****** Object:  Database [Webdictaat.Test]    Script Date: 12-3-2018 17:01:15 ******/
CREATE DATABASE [Webdictaat.Test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Webdictaat.Test', FILENAME = N'C:\Users\Stijn\Webdictaat.Test.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Webdictaat.Test_log', FILENAME = N'C:\Users\Stijn\Webdictaat.Test_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Webdictaat.Test] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Webdictaat.Test].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Webdictaat.Test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET ARITHABORT OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Webdictaat.Test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Webdictaat.Test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Webdictaat.Test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Webdictaat.Test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Webdictaat.Test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Webdictaat.Test] SET  MULTI_USER 
GO
ALTER DATABASE [Webdictaat.Test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Webdictaat.Test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Webdictaat.Test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Webdictaat.Test] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Webdictaat.Test] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Webdictaat.Test] SET QUERY_STORE = OFF
GO
USE [Webdictaat.Test]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Webdictaat.Test]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Achievements]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Achievements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DictaatName] [nvarchar](max) NULL,
	[Hidden] [bit] NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Achievements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsCorrect] [bit] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[FullName] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assignments]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assignments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[DictaatDetailsId] [nvarchar](450) NOT NULL,
	[Metadata] [nvarchar](max) NULL,
	[Points] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[AssignmentSecret] [nvarchar](max) NULL,
	[Level] [int] NOT NULL,
	[ExternalId] [nvarchar](max) NULL,
	[AssignmentType] [int] NULL,
 CONSTRAINT [PK_Assignments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssignmentSubmissions]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignmentSubmissions](
	[AssignmentId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[PointsRecieved] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[Accepted] [bit] NOT NULL,
 CONSTRAINT [PK_AssignmentSubmissions] PRIMARY KEY CLUSTERED 
(
	[AssignmentId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DictaatAchievements]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictaatAchievements](
	[DictaatName] [nvarchar](450) NOT NULL,
	[AchievementId] [int] NOT NULL,
	[GroupName] [nvarchar](max) NOT NULL,
	[GroupOrder] [int] NOT NULL,
 CONSTRAINT [PK_DictaatAchievements] PRIMARY KEY CLUSTERED 
(
	[DictaatName] ASC,
	[AchievementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DictaatContributer]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictaatContributer](
	[UserId] [nvarchar](450) NOT NULL,
	[DictaatDetailsId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_DictaatContributer] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[DictaatDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DictaatDetails]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictaatDetails](
	[Name] [nvarchar](450) NOT NULL,
	[DictaatOwnerId] [nvarchar](450) NOT NULL,
	[IsEnabled] [bit] NOT NULL,
 CONSTRAINT [PK_DictaatDetails] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DictaatGroup]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictaatGroup](
	[Name] [nvarchar](450) NOT NULL,
	[DictaatName] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_DictaatGroup] PRIMARY KEY CLUSTERED 
(
	[Name] ASC,
	[DictaatName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DictaatSession]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictaatSession](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DictaatDetailsId] [nvarchar](450) NULL,
	[EndedOn] [datetime2](7) NULL,
	[StartedOn] [datetime2](7) NULL,
 CONSTRAINT [PK_DictaatSession] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DictaatSessionUser]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DictaatSessionUser](
	[UserId] [nvarchar](450) NOT NULL,
	[DictaatSessionId] [int] NOT NULL,
	[Group] [nvarchar](450) NULL,
	[DictaatName] [nvarchar](450) NULL,
 CONSTRAINT [PK_DictaatSessionUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[DictaatSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PollOption]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PollOption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PollId] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
 CONSTRAINT [PK_PollOption] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Polls]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Polls](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[DictaatName] [nvarchar](max) NOT NULL,
	[Question] [nvarchar](max) NULL,
 CONSTRAINT [PK_Polls] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PollVotes]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PollVotes](
	[PollId] [int] NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[PollOptionId] [int] NOT NULL,
 CONSTRAINT [PK_PollVotes] PRIMARY KEY CLUSTERED 
(
	[PollId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionQuiz]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionQuiz](
	[QuestionId] [int] NOT NULL,
	[QuizId] [int] NOT NULL,
 CONSTRAINT [PK_QuestionQuiz] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC,
	[QuizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[Body] [nvarchar](max) NULL,
	[QuestionType] [nvarchar](max) NULL,
	[Explanation] [nvarchar](max) NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizAttemptQuestion]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizAttemptQuestion](
	[QuizAttemptId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[IsCorrect] [bit] NOT NULL,
 CONSTRAINT [PK_QuizAttemptQuestion] PRIMARY KEY CLUSTERED 
(
	[QuizAttemptId] ASC,
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizAttempts]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizAttempts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuizId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[UserId] [nvarchar](max) NULL,
 CONSTRAINT [PK_QuizAttempts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quizes]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quizes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[DictaatDetailsName] [nvarchar](max) NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Shuffle] [bit] NOT NULL,
	[AssignmentId] [int] NULL,
 CONSTRAINT [PK_Quizes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rates]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Emotion] [int] NOT NULL,
	[Feedback] [nvarchar](max) NOT NULL,
	[RatingId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
	[UserId] [nvarchar](max) NULL,
 CONSTRAINT [PK_Rates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ratings]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ratings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[DictaatDetailsName] [nvarchar](max) NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Ratings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAchievements]    Script Date: 12-3-2018 17:01:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAchievements](
	[UserId] [nvarchar](450) NOT NULL,
	[AchievementId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserAchievements] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[AchievementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170313142102_initial', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170329121700_Quizes', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170424121835_assignments', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170508091129_dictaat contributers', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170515091523_fix voor dictaat owner', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170515131657_delete flag for quiz items', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170526101048_Achievements', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170529100519_dictaat_sessions', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170529132436_assignment_level', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170607092924_foreignkey_assignments', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170626154127_Userachievements', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170824200255_AssignmentExternalId', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170828134117_SessionUserGroup', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170909124919_disabledictaat', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170922133301_FullName', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20170929113314_accept-submission', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171029175609_RemoveAnswerAddBody', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171106142503_question-explanation', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171106152401_question-assignment', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171106160338_assignment-type', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171113104835_descnullable', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20171218101023_cascade-delete', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20180126122402_groupwithid', N'2.0.1-rtm-125')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20180226124508_polls', N'2.0.1-rtm-125')
GO
INSERT [dbo].[AspNetUserLogins] ([LoginProvider], [ProviderKey], [ProviderDisplayName], [UserId]) VALUES (N'Google', N'102868865140129094797', N'Google', N'bc98220a-5550-4ef9-8755-1997a919732c')
GO
INSERT [dbo].[AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName], [FullName]) VALUES (N'bc98220a-5550-4ef9-8755-1997a919732c', 0, N'516c6f9b-6785-4fd6-ae44-bd23c7d630ba', N'ssmulder@avans.nl', 0, 1, NULL, N'SSMULDER@AVANS.NL', N'SSMULDER', NULL, NULL, 0, N'e9a557c2-f354-4863-89c5-2cf2fe914a1d', 0, N'ssmulder', N'Stijn Smulders')
GO
SET IDENTITY_INSERT [dbo].[Assignments] ON 
GO
INSERT [dbo].[Assignments] ([Id], [Description], [DictaatDetailsId], [Metadata], [Points], [Title], [AssignmentSecret], [Level], [ExternalId], [AssignmentType]) VALUES (1, N'Quiz A Description', N'Test', N'1.1', 10, N'Quiz A', NULL, 1, NULL, 1)
GO
INSERT [dbo].[Assignments] ([Id], [Description], [DictaatDetailsId], [Metadata], [Points], [Title], [AssignmentSecret], [Level], [ExternalId], [AssignmentType]) VALUES (2, N'Assignment A Description', N'Test', N'1.2', 20, N'Assignment A', NULL, 1, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Assignments] OFF
GO
INSERT [dbo].[DictaatDetails] ([Name], [DictaatOwnerId], [IsEnabled]) VALUES (N'Test', N'bc98220a-5550-4ef9-8755-1997a919732c', 0)
GO
SET IDENTITY_INSERT [dbo].[DictaatSession] ON 
GO
INSERT [dbo].[DictaatSession] ([Id], [DictaatDetailsId], [EndedOn], [StartedOn]) VALUES (1, N'Test', NULL, CAST(N'2018-03-12T14:32:00.9833121' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[DictaatSession] OFF
GO
INSERT [dbo].[QuestionQuiz] ([QuestionId], [QuizId]) VALUES (1, 1)
GO
INSERT [dbo].[QuestionQuiz] ([QuestionId], [QuizId]) VALUES (2, 1)
GO
INSERT [dbo].[QuestionQuiz] ([QuestionId], [QuizId]) VALUES (3, 1)
GO
INSERT [dbo].[QuestionQuiz] ([QuestionId], [QuizId]) VALUES (4, 1)
GO
SET IDENTITY_INSERT [dbo].[Questions] ON 
GO
INSERT [dbo].[Questions] ([Id], [Text], [IsDeleted], [Body], [QuestionType], [Explanation]) VALUES (1, N'Quiz A Question 1', 0, N'{"answers":[{"text":"A"},{"text":"B"},{"text":"C"},{"text":"D"}]}', N'mc', N'Never wrong A.1')
GO
INSERT [dbo].[Questions] ([Id], [Text], [IsDeleted], [Body], [QuestionType], [Explanation]) VALUES (2, N'Quiz A Question 2', 0, N'{"answers":[{"text":"A"},{"text":"B"},{"text":"C"},{"text":"D"}]}', N'group', N'Never wrong A.2')
GO
INSERT [dbo].[Questions] ([Id], [Text], [IsDeleted], [Body], [QuestionType], [Explanation]) VALUES (3, N'Quiz A Question 3', 0, N'{"sentence":"first A then B then C then D","answers":[{"text":"E"}]}', N'sentence', N'Never wrong A.3')
GO
INSERT [dbo].[Questions] ([Id], [Text], [IsDeleted], [Body], [QuestionType], [Explanation]) VALUES (4, N'Quiz A Question 4', 0, N'{"sentence":"first [[A]] then [[B]] then [[C]] then [[D]]","answers":[{"text":"E"}]}', N'blanks', N'Never wrong A.4')
GO
SET IDENTITY_INSERT [dbo].[Questions] OFF
GO
SET IDENTITY_INSERT [dbo].[Quizes] ON 
GO
INSERT [dbo].[Quizes] ([Id], [Description], [DictaatDetailsName], [Timestamp], [Title], [Shuffle], [AssignmentId]) VALUES (1, NULL, N'Test', CAST(N'2018-03-12T14:34:39.7376141' AS DateTime2), NULL, 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Quizes] OFF
GO
SET IDENTITY_INSERT [dbo].[Polls] ON 
GO
INSERT [dbo].[Polls] ([Id], [IsDeleted], [DictaatName], [Question]) VALUES (1, 0, N'Test', N'Poll A')
GO
SET IDENTITY_INSERT [dbo].[Polls] OFF
GO
SET IDENTITY_INSERT [dbo].[PollOption] ON 
GO
INSERT [dbo].[PollOption] ([Id], [PollId], [Text]) VALUES (1, 1, N'Poll option A')
GO
INSERT [dbo].[PollOption] ([Id], [PollId], [Text]) VALUES (2, 1, N'Poll option B ')
GO
INSERT [dbo].[PollOption] ([Id], [PollId], [Text]) VALUES (3, 1, N'Poll option C ')
GO
SET IDENTITY_INSERT [dbo].[PollOption] OFF
GO
INSERT [dbo].[PollVotes] ([PollId], [UserId], [PollOptionId]) VALUES (1, N'bc98220a-5550-4ef9-8755-1997a919732c', 1)
/****** Object:  Index [IX_Answer_QuestionId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_Answer_QuestionId] ON [dbo].[Answer]
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 12-3-2018 17:01:16 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 12-3-2018 17:01:16 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Assignments_DictaatDetailsId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_Assignments_DictaatDetailsId] ON [dbo].[Assignments]
(
	[DictaatDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DictaatAchievements_AchievementId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_DictaatAchievements_AchievementId] ON [dbo].[DictaatAchievements]
(
	[AchievementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DictaatContributer_DictaatDetailsId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_DictaatContributer_DictaatDetailsId] ON [dbo].[DictaatContributer]
(
	[DictaatDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DictaatDetails_DictaatOwnerId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_DictaatDetails_DictaatOwnerId] ON [dbo].[DictaatDetails]
(
	[DictaatOwnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DictaatGroup_DictaatName]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_DictaatGroup_DictaatName] ON [dbo].[DictaatGroup]
(
	[DictaatName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DictaatSession_DictaatDetailsId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_DictaatSession_DictaatDetailsId] ON [dbo].[DictaatSession]
(
	[DictaatDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DictaatSessionUser_DictaatSessionId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_DictaatSessionUser_DictaatSessionId] ON [dbo].[DictaatSessionUser]
(
	[DictaatSessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DictaatSessionUser_Group_DictaatName]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_DictaatSessionUser_Group_DictaatName] ON [dbo].[DictaatSessionUser]
(
	[Group] ASC,
	[DictaatName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PollOption_PollId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_PollOption_PollId] ON [dbo].[PollOption]
(
	[PollId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PollVotes_PollOptionId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_PollVotes_PollOptionId] ON [dbo].[PollVotes]
(
	[PollOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuestionQuiz_QuizId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_QuestionQuiz_QuizId] ON [dbo].[QuestionQuiz]
(
	[QuizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuizAttemptQuestion_QuestionId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_QuizAttemptQuestion_QuestionId] ON [dbo].[QuizAttemptQuestion]
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_QuizAttempts_QuizId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_QuizAttempts_QuizId] ON [dbo].[QuizAttempts]
(
	[QuizId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Quizes_AssignmentId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_Quizes_AssignmentId] ON [dbo].[Quizes]
(
	[AssignmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Rates_RatingId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_Rates_RatingId] ON [dbo].[Rates]
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserAchievements_AchievementId]    Script Date: 12-3-2018 17:01:16 ******/
CREATE NONCLUSTERED INDEX [IX_UserAchievements_AchievementId] ON [dbo].[UserAchievements]
(
	[AchievementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Answer] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Assignments] ADD  DEFAULT ((0)) FOR [Level]
GO
ALTER TABLE [dbo].[AssignmentSubmissions] ADD  DEFAULT ((0)) FOR [Accepted]
GO
ALTER TABLE [dbo].[DictaatDetails] ADD  DEFAULT ((0)) FOR [IsEnabled]
GO
ALTER TABLE [dbo].[Questions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Quizes] ADD  DEFAULT ((0)) FOR [Shuffle]
GO
ALTER TABLE [dbo].[Ratings] ADD  DEFAULT (N'') FOR [DictaatDetailsName]
GO
ALTER TABLE [dbo].[Ratings] ADD  DEFAULT ('0001-01-01T00:00:00.000') FOR [Timestamp]
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_Questions_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_Questions_QuestionId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Assignments]  WITH CHECK ADD  CONSTRAINT [FK_Assignments_DictaatDetails_DictaatDetailsId] FOREIGN KEY([DictaatDetailsId])
REFERENCES [dbo].[DictaatDetails] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Assignments] CHECK CONSTRAINT [FK_Assignments_DictaatDetails_DictaatDetailsId]
GO
ALTER TABLE [dbo].[AssignmentSubmissions]  WITH CHECK ADD  CONSTRAINT [FK_AssignmentSubmissions_Assignments_AssignmentId] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AssignmentSubmissions] CHECK CONSTRAINT [FK_AssignmentSubmissions_Assignments_AssignmentId]
GO
ALTER TABLE [dbo].[DictaatAchievements]  WITH CHECK ADD  CONSTRAINT [FK_DictaatAchievements_Achievements_AchievementId] FOREIGN KEY([AchievementId])
REFERENCES [dbo].[Achievements] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DictaatAchievements] CHECK CONSTRAINT [FK_DictaatAchievements_Achievements_AchievementId]
GO
ALTER TABLE [dbo].[DictaatAchievements]  WITH CHECK ADD  CONSTRAINT [FK_DictaatAchievements_DictaatDetails_DictaatName] FOREIGN KEY([DictaatName])
REFERENCES [dbo].[DictaatDetails] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DictaatAchievements] CHECK CONSTRAINT [FK_DictaatAchievements_DictaatDetails_DictaatName]
GO
ALTER TABLE [dbo].[DictaatContributer]  WITH CHECK ADD  CONSTRAINT [FK_DictaatContributer_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DictaatContributer] CHECK CONSTRAINT [FK_DictaatContributer_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[DictaatContributer]  WITH CHECK ADD  CONSTRAINT [FK_DictaatContributer_DictaatDetails_DictaatDetailsId] FOREIGN KEY([DictaatDetailsId])
REFERENCES [dbo].[DictaatDetails] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DictaatContributer] CHECK CONSTRAINT [FK_DictaatContributer_DictaatDetails_DictaatDetailsId]
GO
ALTER TABLE [dbo].[DictaatDetails]  WITH CHECK ADD  CONSTRAINT [FK_DictaatDetails_AspNetUsers_DictaatOwnerId] FOREIGN KEY([DictaatOwnerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[DictaatDetails] CHECK CONSTRAINT [FK_DictaatDetails_AspNetUsers_DictaatOwnerId]
GO
ALTER TABLE [dbo].[DictaatGroup]  WITH CHECK ADD  CONSTRAINT [FK_DictaatGroup_DictaatDetails_DictaatName] FOREIGN KEY([DictaatName])
REFERENCES [dbo].[DictaatDetails] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DictaatGroup] CHECK CONSTRAINT [FK_DictaatGroup_DictaatDetails_DictaatName]
GO
ALTER TABLE [dbo].[DictaatSession]  WITH CHECK ADD  CONSTRAINT [FK_DictaatSession_DictaatDetails_DictaatDetailsId] FOREIGN KEY([DictaatDetailsId])
REFERENCES [dbo].[DictaatDetails] ([Name])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DictaatSession] CHECK CONSTRAINT [FK_DictaatSession_DictaatDetails_DictaatDetailsId]
GO
ALTER TABLE [dbo].[DictaatSessionUser]  WITH CHECK ADD  CONSTRAINT [FK_DictaatSessionUser_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DictaatSessionUser] CHECK CONSTRAINT [FK_DictaatSessionUser_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[DictaatSessionUser]  WITH CHECK ADD  CONSTRAINT [FK_DictaatSessionUser_DictaatGroup_Group_DictaatName] FOREIGN KEY([Group], [DictaatName])
REFERENCES [dbo].[DictaatGroup] ([Name], [DictaatName])
GO
ALTER TABLE [dbo].[DictaatSessionUser] CHECK CONSTRAINT [FK_DictaatSessionUser_DictaatGroup_Group_DictaatName]
GO
ALTER TABLE [dbo].[DictaatSessionUser]  WITH CHECK ADD  CONSTRAINT [FK_DictaatSessionUser_DictaatSession_DictaatSessionId] FOREIGN KEY([DictaatSessionId])
REFERENCES [dbo].[DictaatSession] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DictaatSessionUser] CHECK CONSTRAINT [FK_DictaatSessionUser_DictaatSession_DictaatSessionId]
GO
ALTER TABLE [dbo].[PollOption]  WITH CHECK ADD  CONSTRAINT [FK_PollOption_Polls_PollId] FOREIGN KEY([PollId])
REFERENCES [dbo].[Polls] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PollOption] CHECK CONSTRAINT [FK_PollOption_Polls_PollId]
GO
ALTER TABLE [dbo].[PollVotes]  WITH CHECK ADD  CONSTRAINT [FK_PollVotes_PollOption_PollOptionId] FOREIGN KEY([PollOptionId])
REFERENCES [dbo].[PollOption] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PollVotes] CHECK CONSTRAINT [FK_PollVotes_PollOption_PollOptionId]
GO
ALTER TABLE [dbo].[QuestionQuiz]  WITH CHECK ADD  CONSTRAINT [FK_QuestionQuiz_Questions_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuestionQuiz] CHECK CONSTRAINT [FK_QuestionQuiz_Questions_QuestionId]
GO
ALTER TABLE [dbo].[QuestionQuiz]  WITH CHECK ADD  CONSTRAINT [FK_QuestionQuiz_Quizes_QuizId] FOREIGN KEY([QuizId])
REFERENCES [dbo].[Quizes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuestionQuiz] CHECK CONSTRAINT [FK_QuestionQuiz_Quizes_QuizId]
GO
ALTER TABLE [dbo].[QuizAttemptQuestion]  WITH CHECK ADD  CONSTRAINT [FK_QuizAttemptQuestion_Questions_QuestionId] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[Questions] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuizAttemptQuestion] CHECK CONSTRAINT [FK_QuizAttemptQuestion_Questions_QuestionId]
GO
ALTER TABLE [dbo].[QuizAttemptQuestion]  WITH CHECK ADD  CONSTRAINT [FK_QuizAttemptQuestion_QuizAttempts_QuizAttemptId] FOREIGN KEY([QuizAttemptId])
REFERENCES [dbo].[QuizAttempts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuizAttemptQuestion] CHECK CONSTRAINT [FK_QuizAttemptQuestion_QuizAttempts_QuizAttemptId]
GO
ALTER TABLE [dbo].[QuizAttempts]  WITH CHECK ADD  CONSTRAINT [FK_QuizAttempts_Quizes_QuizId] FOREIGN KEY([QuizId])
REFERENCES [dbo].[Quizes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuizAttempts] CHECK CONSTRAINT [FK_QuizAttempts_Quizes_QuizId]
GO
ALTER TABLE [dbo].[Quizes]  WITH CHECK ADD  CONSTRAINT [FK_Quizes_Assignments_AssignmentId] FOREIGN KEY([AssignmentId])
REFERENCES [dbo].[Assignments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Quizes] CHECK CONSTRAINT [FK_Quizes_Assignments_AssignmentId]
GO
ALTER TABLE [dbo].[Rates]  WITH CHECK ADD  CONSTRAINT [FK_Rates_Ratings_RatingId] FOREIGN KEY([RatingId])
REFERENCES [dbo].[Ratings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rates] CHECK CONSTRAINT [FK_Rates_Ratings_RatingId]
GO
ALTER TABLE [dbo].[UserAchievements]  WITH CHECK ADD  CONSTRAINT [FK_UserAchievements_Achievements_AchievementId] FOREIGN KEY([AchievementId])
REFERENCES [dbo].[Achievements] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAchievements] CHECK CONSTRAINT [FK_UserAchievements_Achievements_AchievementId]
GO
ALTER TABLE [dbo].[UserAchievements]  WITH CHECK ADD  CONSTRAINT [FK_UserAchievements_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserAchievements] CHECK CONSTRAINT [FK_UserAchievements_AspNetUsers_UserId]
GO
USE [master]
GO
ALTER DATABASE [Webdictaat.Test] SET  READ_WRITE 
GO
