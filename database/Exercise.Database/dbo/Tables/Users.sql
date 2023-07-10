CREATE TABLE [dbo].[Users] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Email]            VARCHAR (80)  NOT NULL,
    [Password]         VARCHAR (150) NOT NULL,
    [RegistrationDate] DATETIME      NOT NULL,
    [ModificationDate] DATETIME      NULL,
    [Active]           BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

