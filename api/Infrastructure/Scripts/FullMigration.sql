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

CREATE TABLE [Address] (
    [Id] uniqueidentifier NOT NULL,
    [Street] varchar(120) NOT NULL,
    [Number] int NOT NULL,
    [Complement] varchar(200) NOT NULL,
    [City] varchar(100) NOT NULL,
    [ZipCode] varchar(9) NOT NULL,
    [State] varchar(50) NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Patient] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(150) NOT NULL,
    [Email] varchar(100) NOT NULL,
    [BirthDate] date NULL,
    [Cpf] varchar(11) NOT NULL,
    [Notes] varchar(500) NOT NULL,
    [Gender] int NOT NULL,
    [EmergencyContract] varchar(150) NOT NULL,
    CONSTRAINT [PK_Patient] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Speciality] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(120) NOT NULL,
    CONSTRAINT [PK_Speciality] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [TermsOfUse] (
    [Id] uniqueidentifier NOT NULL,
    [Content] varchar(500) NOT NULL,
    [Name] varchar(150) NOT NULL,
    [CreatedAt] date NOT NULL,
    [Version] varchar(150) NOT NULL,
    CONSTRAINT [PK_TermsOfUse] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Location] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(100) NOT NULL,
    [AddressId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Location_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [User] (
    [Id] uniqueidentifier NOT NULL,
    [Name] varchar(100) NOT NULL,
    [Email] varchar(50) NOT NULL,
    [Gender] int NOT NULL,
    [AddressId] uniqueidentifier NOT NULL,
    [Avatar] varchar(500) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_User_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Consent] (
    [Id] uniqueidentifier NOT NULL,
    [PatientId] uniqueidentifier NOT NULL,
    [ConsentType] varchar(100) NOT NULL,
    [GivenAt] date NOT NULL,
    [Version] string NOT NULL,
    CONSTRAINT [PK_Consent] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Consent_Patient_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patient] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [HealthCareProfissional] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NULL,
    [SpecialityId] uniqueidentifier NOT NULL,
    [Name] varchar(100) NOT NULL,
    [CurriculumURL] varchar(500) NOT NULL,
    [UndergraduateURL] varchar(100) NOT NULL,
    [CrpOrCrmURL] varchar(150) NOT NULL,
    [ApprovalStatus] int NOT NULL,
    [AvailabilityStatus] int NOT NULL,
    CONSTRAINT [PK_HealthCareProfissional] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HealthCareProfissional_Speciality_SpecialityId] FOREIGN KEY ([SpecialityId]) REFERENCES [Speciality] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_HealthCareProfissional_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [TermsAcceptance] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [TermsOfUseId] uniqueidentifier NOT NULL,
    [CreatedAt] date NOT NULL,
    CONSTRAINT [PK_TermsAcceptance] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TermsAcceptance_TermsOfUse_TermsOfUseId] FOREIGN KEY ([TermsOfUseId]) REFERENCES [TermsOfUse] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TermsAcceptance_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Availabilitie] (
    [Id] uniqueidentifier NOT NULL,
    [PatientId] uniqueidentifier NOT NULL,
    [ProfissionalId] uniqueidentifier NOT NULL,
    [AvaliableAt] date NOT NULL,
    [DurationMinutes] time NOT NULL,
    [BookedByUserId] uniqueidentifier NOT NULL,
    [IsBooked] bool NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [Source] int NOT NULL,
    [TypeAvailabilitie] int NOT NULL,
    [Location] varchar(100) NULL,
    [MeetUrl] varchar(100) NULL,
    CONSTRAINT [PK_Availabilitie] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Availabilitie_HealthCareProfissional_ProfissionalId] FOREIGN KEY ([ProfissionalId]) REFERENCES [HealthCareProfissional] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Availabilitie_Patient_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patient] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Availabilitie_User_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SessionSchedule] (
    [Id] uniqueidentifier NOT NULL,
    [ProfessionalId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [PatientId] uniqueidentifier NOT NULL,
    [AvaliableAt] date NOT NULL,
    [Status] int NOT NULL,
    [DurationMinute] time NOT NULL,
    CONSTRAINT [PK_SessionSchedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SessionSchedule_HealthCareProfissional_ProfessionalId] FOREIGN KEY ([ProfessionalId]) REFERENCES [HealthCareProfissional] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SessionSchedule_Patient_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [Patient] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SessionSchedule_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [SessionNote] (
    [Id] uniqueidentifier NOT NULL,
    [SessionScheduleId] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ProfessionalId] uniqueidentifier NOT NULL,
    [Content] varchar(500) NOT NULL,
    [Tags] varchar(150) NOT NULL,
    [Insight] varchar(100) NOT NULL,
    CONSTRAINT [PK_SessionNote] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SessionNote_HealthCareProfissional_ProfessionalId] FOREIGN KEY ([ProfessionalId]) REFERENCES [HealthCareProfissional] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SessionNote_SessionSchedule_SessionScheduleId] FOREIGN KEY ([SessionScheduleId]) REFERENCES [SessionSchedule] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SessionNote_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Wait] (
    [Id] uniqueidentifier NOT NULL,
    [PatientId] uniqueidentifier NOT NULL,
    [SessionId] uniqueidentifier NOT NULL,
    [PreferrdeTime] date NOT NULL,
    [Status] uniqueidentifier NOT NULL,
    [CreatedaAt] date NOT NULL,
    [UpdatedAt] date NOT NULL,
    CONSTRAINT [PK_Wait] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Wait_Patient_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [Patient] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Wait_SessionSchedule_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [SessionSchedule] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Availabilitie_CreatedBy] ON [Availabilitie] ([CreatedBy]);
GO

CREATE INDEX [IX_Availabilitie_PatientId] ON [Availabilitie] ([PatientId]);
GO

CREATE INDEX [IX_Availabilitie_ProfissionalId] ON [Availabilitie] ([ProfissionalId]);
GO

CREATE INDEX [IX_Consent_PatientId] ON [Consent] ([PatientId]);
GO

CREATE INDEX [IX_HealthCareProfissional_SpecialityId] ON [HealthCareProfissional] ([SpecialityId]);
GO

CREATE INDEX [IX_HealthCareProfissional_UserId] ON [HealthCareProfissional] ([UserId]);
GO

CREATE INDEX [IX_Location_AddressId] ON [Location] ([AddressId]);
GO

CREATE INDEX [IX_SessionNote_ProfessionalId] ON [SessionNote] ([ProfessionalId]);
GO

CREATE INDEX [IX_SessionNote_SessionScheduleId] ON [SessionNote] ([SessionScheduleId]);
GO

CREATE INDEX [IX_SessionNote_UserId] ON [SessionNote] ([UserId]);
GO

CREATE INDEX [IX_SessionSchedule_PatientId] ON [SessionSchedule] ([PatientId]);
GO

CREATE INDEX [IX_SessionSchedule_ProfessionalId] ON [SessionSchedule] ([ProfessionalId]);
GO

CREATE INDEX [IX_SessionSchedule_UserId] ON [SessionSchedule] ([UserId]);
GO

CREATE INDEX [IX_TermsAcceptance_TermsOfUseId] ON [TermsAcceptance] ([TermsOfUseId]);
GO

CREATE INDEX [IX_TermsAcceptance_UserId] ON [TermsAcceptance] ([UserId]);
GO

CREATE INDEX [IX_User_AddressId] ON [User] ([AddressId]);
GO

CREATE INDEX [IX_Wait_SessionId] ON [Wait] ([SessionId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251023230717_InitialCreate', N'8.0.20');
GO

COMMIT;
GO

