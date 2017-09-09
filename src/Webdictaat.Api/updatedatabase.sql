IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Name] nvarchar(256),
    [NormalizedName] nvarchar(256),
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max),
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name])
);

GO

CREATE TABLE [Questions] (
    [Id] int NOT NULL IDENTITY,
    [Text] nvarchar(max),
    CONSTRAINT [PK_Questions] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Ratings] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Ratings] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [ConcurrencyStamp] nvarchar(max),
    [Email] nvarchar(256),
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset,
    [NormalizedEmail] nvarchar(256),
    [NormalizedUserName] nvarchar(256),
    [PasswordHash] nvarchar(max),
    [PhoneNumber] nvarchar(max),
    [PhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(max),
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256),
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Answer] (
    [Id] int NOT NULL IDENTITY,
    [IsCorrect] bit NOT NULL,
    [QuestionId] int NOT NULL,
    [Text] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Answer] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Answer_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Rates] (
    [Id] int NOT NULL IDENTITY,
    [Emotion] int NOT NULL,
    [Feedback] nvarchar(max) NOT NULL,
    [RatingId] int NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    [UserId] nvarchar(max),
    CONSTRAINT [PK_Rates] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Rates_Ratings_RatingId] FOREIGN KEY ([RatingId]) REFERENCES [Ratings] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max),
    [ClaimValue] nvarchar(max),
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max),
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [DictaatDetails] (
    [Name] nvarchar(450) NOT NULL,
    [DictaatOwnerId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_DictaatDetails] PRIMARY KEY ([Name]),
    CONSTRAINT [FK_DictaatDetails_AspNetUsers_DictaatOwnerId] FOREIGN KEY ([DictaatOwnerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [IX_Answer_QuestionId] ON [Answer] ([QuestionId]);

GO

CREATE INDEX [IX_DictaatDetails_DictaatOwnerId] ON [DictaatDetails] ([DictaatOwnerId]);

GO

CREATE INDEX [IX_Rates_RatingId] ON [Rates] ([RatingId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170313142102_initial', N'1.1.1');

GO

ALTER TABLE [Ratings] ADD [DictaatDetailsName] nvarchar(max) NOT NULL DEFAULT N'';

GO

ALTER TABLE [Ratings] ADD [Timestamp] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.000';

GO

CREATE TABLE [Quizes] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [DictaatDetailsName] nvarchar(max) NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    [Title] nvarchar(max),
    CONSTRAINT [PK_Quizes] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [QuizAttempts] (
    [Id] int NOT NULL IDENTITY,
    [QuizId] int NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    [UserId] nvarchar(max),
    CONSTRAINT [PK_QuizAttempts] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [QuestionQuiz] (
    [QuestionId] int NOT NULL,
    [QuizId] int NOT NULL,
    CONSTRAINT [PK_QuestionQuiz] PRIMARY KEY ([QuestionId], [QuizId]),
    CONSTRAINT [FK_QuestionQuiz_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_QuestionQuiz_Quizes_QuizId] FOREIGN KEY ([QuizId]) REFERENCES [Quizes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [QuizAttemptAnswer] (
    [QuizAttemptId] int NOT NULL,
    [AnswerId] int NOT NULL,
    CONSTRAINT [PK_QuizAttemptAnswer] PRIMARY KEY ([QuizAttemptId], [AnswerId]),
    CONSTRAINT [FK_QuizAttemptAnswer_Answer_AnswerId] FOREIGN KEY ([AnswerId]) REFERENCES [Answer] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_QuizAttemptAnswer_QuizAttempts_QuizAttemptId] FOREIGN KEY ([QuizAttemptId]) REFERENCES [QuizAttempts] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_QuestionQuiz_QuizId] ON [QuestionQuiz] ([QuizId]);

GO

CREATE INDEX [IX_QuizAttemptAnswer_AnswerId] ON [QuizAttemptAnswer] ([AnswerId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170329121700_Quizes', N'1.1.1');

GO

CREATE TABLE [Assignments] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NOT NULL,
    [DictaatDetailsName] nvarchar(max) NOT NULL,
    [Metadata] nvarchar(max),
    [Points] int NOT NULL,
    [Title] nvarchar(max),
    [Secret] nvarchar(max),
    CONSTRAINT [PK_Assignments] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AssignmentSubmissions] (
    [AssignmentId] int NOT NULL,
    [UserId] nvarchar(450) NOT NULL,
    [PointsRecieved] int NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_AssignmentSubmissions] PRIMARY KEY ([AssignmentId], [UserId]),
    CONSTRAINT [FK_AssignmentSubmissions_Assignments_AssignmentId] FOREIGN KEY ([AssignmentId]) REFERENCES [Assignments] ([Id]) ON DELETE CASCADE
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170424121835_assignments', N'1.1.1');

GO

ALTER TABLE [DictaatDetails] DROP CONSTRAINT [FK_DictaatDetails_AspNetUsers_DictaatOwnerId];

GO

DROP INDEX [IX_DictaatDetails_DictaatOwnerId] ON [DictaatDetails];

GO

EXEC sp_rename N'Assignments.Secret', N'AssignmentSecret', N'COLUMN';

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'DictaatDetails') AND [c].[name] = N'DictaatOwnerId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [DictaatDetails] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [DictaatDetails] ALTER COLUMN [DictaatOwnerId] nvarchar(max) NOT NULL;

GO

ALTER TABLE [DictaatDetails] ADD [DictaatOwnersId] nvarchar(450);

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Assignments') AND [c].[name] = N'Title');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Assignments] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Assignments] ALTER COLUMN [Title] nvarchar(max) NOT NULL;

GO

CREATE TABLE [DictaatContributer] (
    [UserId] nvarchar(450) NOT NULL,
    [DictaatDetailsId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_DictaatContributer] PRIMARY KEY ([UserId], [DictaatDetailsId]),
    CONSTRAINT [FK_DictaatContributer_DictaatDetails_DictaatDetailsId] FOREIGN KEY ([DictaatDetailsId]) REFERENCES [DictaatDetails] ([Name]) ON DELETE CASCADE,
    CONSTRAINT [FK_DictaatContributer_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_DictaatDetails_DictaatOwnersId] ON [DictaatDetails] ([DictaatOwnersId]);

GO

CREATE INDEX [IX_DictaatContributer_DictaatDetailsId] ON [DictaatContributer] ([DictaatDetailsId]);

GO

ALTER TABLE [DictaatDetails] ADD CONSTRAINT [FK_DictaatDetails_AspNetUsers_DictaatOwnersId] FOREIGN KEY ([DictaatOwnersId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170508091129_dictaat contributers', N'1.1.1');

GO

ALTER TABLE [DictaatDetails] DROP CONSTRAINT [FK_DictaatDetails_AspNetUsers_DictaatOwnersId];

GO

DROP INDEX [IX_DictaatDetails_DictaatOwnersId] ON [DictaatDetails];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'DictaatDetails') AND [c].[name] = N'DictaatOwnersId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [DictaatDetails] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [DictaatDetails] DROP COLUMN [DictaatOwnersId];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'DictaatDetails') AND [c].[name] = N'DictaatOwnerId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [DictaatDetails] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [DictaatDetails] ALTER COLUMN [DictaatOwnerId] nvarchar(450) NOT NULL;

GO

CREATE INDEX [IX_QuizAttempts_QuizId] ON [QuizAttempts] ([QuizId]);

GO

CREATE INDEX [IX_DictaatDetails_DictaatOwnerId] ON [DictaatDetails] ([DictaatOwnerId]);

GO

ALTER TABLE [DictaatDetails] ADD CONSTRAINT [FK_DictaatDetails_AspNetUsers_DictaatOwnerId] FOREIGN KEY ([DictaatOwnerId]) REFERENCES [AspNetUsers] ([Id]);

GO

ALTER TABLE [QuizAttempts] ADD CONSTRAINT [FK_QuizAttempts_Quizes_QuizId] FOREIGN KEY ([QuizId]) REFERENCES [Quizes] ([Id]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170515091523_fix voor dictaat owner', N'1.1.1');

GO

ALTER TABLE [Questions] ADD [IsDeleted] bit NOT NULL DEFAULT 0;

GO

ALTER TABLE [Answer] ADD [IsDeleted] bit NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170515131657_delete flag for quiz items', N'1.1.1');

GO

CREATE TABLE [Achievements] (
    [Id] int NOT NULL IDENTITY,
    [DictaatName] nvarchar(max),
    [Hidden] bit NOT NULL,
    [Image] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Achievements] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [DictaatAchievements] (
    [DictaatName] nvarchar(450) NOT NULL,
    [AchievementId] int NOT NULL,
    [GroupName] nvarchar(max) NOT NULL,
    [GroupOrder] int NOT NULL,
    CONSTRAINT [PK_DictaatAchievements] PRIMARY KEY ([DictaatName], [AchievementId])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170526101048_Achievements', N'1.1.1');

GO

CREATE TABLE [DictaatSession] (
    [Id] int NOT NULL IDENTITY,
    [DictaatDetailsId] nvarchar(450),
    [EndedOn] datetime2,
    [StartedOn] datetime2,
    CONSTRAINT [PK_DictaatSession] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DictaatSession_DictaatDetails_DictaatDetailsId] FOREIGN KEY ([DictaatDetailsId]) REFERENCES [DictaatDetails] ([Name]) ON DELETE NO ACTION
);

GO

CREATE TABLE [DictaatSessionUser] (
    [UserId] nvarchar(450) NOT NULL,
    [DictaatSessionId] int NOT NULL,
    CONSTRAINT [PK_DictaatSessionUser] PRIMARY KEY ([UserId], [DictaatSessionId]),
    CONSTRAINT [FK_DictaatSessionUser_DictaatSession_DictaatSessionId] FOREIGN KEY ([DictaatSessionId]) REFERENCES [DictaatSession] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DictaatSessionUser_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_DictaatSession_DictaatDetailsId] ON [DictaatSession] ([DictaatDetailsId]);

GO

CREATE INDEX [IX_DictaatSessionUser_DictaatSessionId] ON [DictaatSessionUser] ([DictaatSessionId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170529100519_dictaat_sessions', N'1.1.1');

GO

ALTER TABLE [Assignments] ADD [Level] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170529132436_assignment_level', N'1.1.1');

GO

EXEC sp_rename N'Assignments.DictaatDetailsName', N'DictaatDetailsId', N'COLUMN';

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'DictaatSession') AND [c].[name] = N'StartedOn');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [DictaatSession] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [DictaatSession] ALTER COLUMN [StartedOn] datetime2;

GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'DictaatSession') AND [c].[name] = N'EndedOn');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [DictaatSession] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [DictaatSession] ALTER COLUMN [EndedOn] datetime2;

GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'Assignments') AND [c].[name] = N'DictaatDetailsId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Assignments] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Assignments] ALTER COLUMN [DictaatDetailsId] nvarchar(450) NOT NULL;

GO

CREATE INDEX [IX_DictaatAchievements_AchievementId] ON [DictaatAchievements] ([AchievementId]);

GO

CREATE INDEX [IX_Assignments_DictaatDetailsId] ON [Assignments] ([DictaatDetailsId]);

GO

ALTER TABLE [Assignments] ADD CONSTRAINT [FK_Assignments_DictaatDetails_DictaatDetailsId] FOREIGN KEY ([DictaatDetailsId]) REFERENCES [DictaatDetails] ([Name]) ON DELETE CASCADE;

GO

ALTER TABLE [DictaatAchievements] ADD CONSTRAINT [FK_DictaatAchievements_Achievements_AchievementId] FOREIGN KEY ([AchievementId]) REFERENCES [Achievements] ([Id]) ON DELETE CASCADE;

GO

ALTER TABLE [DictaatAchievements] ADD CONSTRAINT [FK_DictaatAchievements_DictaatDetails_DictaatName] FOREIGN KEY ([DictaatName]) REFERENCES [DictaatDetails] ([Name]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170607092924_foreignkey_assignments', N'1.1.1');

GO

CREATE TABLE [UserAchievements] (
    [UserId] nvarchar(450) NOT NULL,
    [AchievementId] int NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_UserAchievements] PRIMARY KEY ([UserId], [AchievementId]),
    CONSTRAINT [FK_UserAchievements_Achievements_AchievementId] FOREIGN KEY ([AchievementId]) REFERENCES [Achievements] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserAchievements_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_UserAchievements_AchievementId] ON [UserAchievements] ([AchievementId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170626154127_Userachievements', N'1.1.1');

GO

ALTER TABLE [Assignments] ADD [ExternalId] nvarchar(max);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170824200255_AssignmentExternalId', N'1.1.1');

GO

ALTER TABLE [DictaatSessionUser] ADD [Group] nvarchar(max);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170828134117_SessionUserGroup', N'1.1.1');

GO

ALTER TABLE [DictaatDetails] ADD [IsEnabled] bit NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20170909124919_disabledictaat', N'1.1.1');

GO

