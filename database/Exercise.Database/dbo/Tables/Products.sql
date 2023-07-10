CREATE TABLE [dbo].[Products] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [Name]             VARCHAR (100)   NOT NULL,
    [Description]      VARCHAR (500)   NULL,
    [Price]            DECIMAL (19, 2) NOT NULL,
    [Quantity]         INT             NOT NULL,
    [RegistrationUser] VARCHAR (80)    NOT NULL,
    [RegistrationDate] DATETIME        NOT NULL,
    [ModificationUser] VARCHAR (80)    NULL,
    [ModificationDate] DATETIME        NULL,
    [Active]           BIT             NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

