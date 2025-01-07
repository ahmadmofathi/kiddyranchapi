IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Discriminator] nvarchar(5) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] varchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] varchar(256) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] varchar(256) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [Branches] (
        [branchId] nvarchar(450) NOT NULL,
        [branchName] nvarchar(max) NOT NULL,
        [locationDescription] nvarchar(max) NULL,
        [locationLink] nvarchar(max) NULL,
        [whatsAppLink] nvarchar(max) NULL,
        [whatsAppNum] nvarchar(max) NOT NULL,
        [slogan] nvarchar(max) NULL,
        [facebook] nvarchar(max) NULL,
        [instagram] nvarchar(max) NULL,
        [tiktok] nvarchar(max) NULL,
        [logo1] nvarchar(max) NOT NULL,
        [logo2] nvarchar(max) NULL,
        [color1] nvarchar(max) NOT NULL,
        [color2] nvarchar(max) NOT NULL,
        [color3] nvarchar(max) NULL,
        [startedAt] datetime2 NOT NULL,
        [payment] nvarchar(max) NULL,
        CONSTRAINT [PK_Branches] PRIMARY KEY ([branchId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [Calls] (
        [callId] nvarchar(450) NOT NULL,
        [callerName] nvarchar(max) NOT NULL,
        [calledId] nvarchar(max) NOT NULL,
        [callDescription] nvarchar(max) NOT NULL,
        [callDate] nvarchar(max) NOT NULL,
        [creationDate] datetime2 NOT NULL,
        [updatedDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Calls] PRIMARY KEY ([callId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [CareerInterviews] (
        [careerInterviewId] nvarchar(450) NOT NULL,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [JobTitle] nvarchar(max) NOT NULL,
        [NID] nvarchar(max) NOT NULL,
        [Adderss] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [Birthday] datetime2 NOT NULL,
        [WhatsApp] nvarchar(max) NOT NULL,
        [CvDescription] nvarchar(max) NOT NULL,
        [Education] nvarchar(max) NOT NULL,
        [InterviewDate] datetime2 NULL,
        [Status] nvarchar(max) NULL,
        [creationDate] datetime2 NULL,
        [updateDate] datetime2 NULL,
        [comment] nvarchar(max) NULL,
        CONSTRAINT [PK_CareerInterviews] PRIMARY KEY ([careerInterviewId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [Employees] (
        [EmployeeId] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Role] nvarchar(max) NOT NULL,
        [bioDescription] nvarchar(max) NOT NULL,
        [profilePic] nvarchar(max) NULL,
        CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [Sections] (
        [sectionId] nvarchar(450) NOT NULL,
        [sectionTitle] nvarchar(max) NOT NULL,
        [sectionSubtitle] nvarchar(max) NULL,
        [sectionDescription] nvarchar(max) NOT NULL,
        [styleCode] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Sections] PRIMARY KEY ([sectionId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [studentInterviews] (
        [StudentInterviewId] nvarchar(450) NOT NULL,
        [StudentName] nvarchar(max) NOT NULL,
        [FatherName] nvarchar(max) NOT NULL,
        [FatherJob] nvarchar(max) NOT NULL,
        [MotherName] nvarchar(max) NOT NULL,
        [MotherJob] nvarchar(max) NULL,
        [Birthday] datetime2 NOT NULL,
        [ParentNID] nvarchar(max) NULL,
        [PhoneNumber1] nvarchar(max) NOT NULL,
        [PhoneNumber2] nvarchar(max) NULL,
        [WhatsApp] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NULL,
        [WantedCourse] nvarchar(max) NULL,
        [StageToJoin] nvarchar(max) NULL,
        [branch] nvarchar(max) NOT NULL,
        [status] nvarchar(max) NOT NULL,
        [creationDate] datetime2 NULL,
        [updateDate] datetime2 NULL,
        [comment] nvarchar(max) NULL,
        [interviewDate] datetime2 NULL,
        CONSTRAINT [PK_studentInterviews] PRIMARY KEY ([StudentInterviewId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(450) NOT NULL,
        [ProviderKey] nvarchar(450) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(450) NOT NULL,
        [Name] nvarchar(450) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE TABLE [Images] (
        [imageFileId] nvarchar(450) NOT NULL,
        [imgName] nvarchar(max) NOT NULL,
        [imgDescription] nvarchar(max) NOT NULL,
        [imgPath] nvarchar(max) NOT NULL,
        [sectionId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_Images] PRIMARY KEY ([imageFileId]),
        CONSTRAINT [FK_Images_Sections_sectionId] FOREIGN KEY ([sectionId]) REFERENCES [Sections] ([sectionId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_AspNetUsers_Email] ON [AspNetUsers] ([Email]) WHERE [Email] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_AspNetUsers_PhoneNumber] ON [AspNetUsers] ([PhoneNumber]) WHERE [PhoneNumber] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [IX_AspNetUsers_UserName] ON [AspNetUsers] ([UserName]) WHERE [UserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Images_sectionId] ON [Images] ([sectionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240824133754_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240824133754_InitialCreate', N'8.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240831173426_unicode'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240831173426_unicode', N'8.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241022093325_income'
)
BEGIN
    CREATE TABLE [Incomes] (
        [IncomeId] nvarchar(450) NOT NULL,
        [IncomeName] nvarchar(max) NULL,
        [IncomeType] nvarchar(max) NULL,
        [IncomeDescription] nvarchar(max) NULL,
        [Branch] nvarchar(max) NULL,
        [PaymentType] nvarchar(max) NULL,
        [PaymentAmount] int NULL,
        [TotalPaymentAmount] int NULL,
        [RemainingAmount] int NULL,
        [RemainingDeadlineDate] datetime2 NULL,
        [PaymentDate] datetime2 NULL,
        [nextPaymentDate] datetime2 NULL,
        CONSTRAINT [PK_Incomes] PRIMARY KEY ([IncomeId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241022093325_income'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241022093325_income', N'8.0.7');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241205073502_test'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241205073502_test', N'8.0.7');
END;
GO

COMMIT;
GO

