USE [master]
GO
/****** Object:  Database [exercisedb]    Script Date: 10/07/2023 01:59:57 ******/
CREATE DATABASE [exercisedb]
ALTER DATABASE [exercisedb] SET QUERY_STORE = OFF
GO
USE [exercisedb]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](500) NULL,
	[Price] [decimal](19, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[RegistrationUser] [varchar](80) NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[ModificationUser] [varchar](80) NULL,
	[ModificationDate] [datetime] NULL,
	[Active] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](80) NOT NULL,
	[Password] [varchar](150) NOT NULL,
	[RegistrationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NULL,
	[Active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [Quantity], [RegistrationUser], [RegistrationDate], [ModificationUser], [ModificationDate], [Active]) VALUES (1, N'MSI Stealth GS77 17.3', N' Seal is opened for Hardware/Software upgrade only to enhance performance.', CAST(10450.50 AS Decimal(19, 2)), 10, N'eflorespalma@gmail.com', CAST(N'2023-07-10T01:34:01.823' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [Quantity], [RegistrationUser], [RegistrationDate], [ModificationUser], [ModificationDate], [Active]) VALUES (2, N'Skullcandy Crusher Evo', N'Listen to songs the way they were made to be heard.', CAST(151.63 AS Decimal(19, 2)), 14, N'eflorespalma@gmail.com', CAST(N'2023-07-10T01:35:27.173' AS DateTime), N'eflorespalma@gmail.com', CAST(N'2023-07-10T01:52:58.580' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Email], [Password], [RegistrationDate], [ModificationDate], [Active]) VALUES (1, N'eflorespalma@gmail.com', N'$2a$11$N.sqpC7UPsHLlrsAi2BNS.OrkfMIa4CEDE6M9TyAYKy0ccj1VYQs6', CAST(N'2023-07-10T01:31:20.307' AS DateTime), NULL, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteProduct]
(
		@Id INT,
		@ModificationUser VARCHAR(80),
		@ModificationDate DATETIME
)
AS
BEGIN
		UPDATE Products
			SET Active = 0,
				ModificationDate = @ModificationDate,
				ModificationUser = @ModificationUser
			 WHERE Id = @Id;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetProductById]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProductById]
(
	@Id INT
)
AS
BEGIN
	SELECT Name, Description, Price, Quantity, Active FROM Products
	WHERE Id = @Id
END;
GO
/****** Object:  StoredProcedure [dbo].[GetProducts]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProducts]
AS
BEGIN
	SELECT Id, Name, Description, Price, Quantity, Active FROM Products
END;
GO
/****** Object:  StoredProcedure [dbo].[GetUserByAccount]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUserByAccount](
	@Email VARCHAR(80)
)
AS
BEGIN

SELECT Email, Password, Active FROM Users where Email = @Email
	
END;
GO
/****** Object:  StoredProcedure [dbo].[ModifyProduct]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ModifyProduct]
(
	@Id INT,
	@Name VARCHAR(100),
	@Description VARCHAR(500),
	@Price DECIMAL(19,2),
	@Quantity INT,
	@ModificationUser VARCHAR(80),
	@ModificationDate DATETIME,
	@Active BIT
)
AS
BEGIN

	UPDATE Product
		SET Name = @Name,
			Description = @Description,
			Price = @Price,
			Quantity = @Quantity,
			ModificationUser = @ModificationUser,
			ModificationDate = @ModificationDate
	 WHERE Id = @Id

END;
GO
/****** Object:  StoredProcedure [dbo].[RegisterProduct]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegisterProduct]
(
	@Name VARCHAR(100),
	@Description VARCHAR(500),
	@Price DECIMAL(19,2),
	@Quantity INT,
	@RegistrationUser VARCHAR(80),
	@RegistrationDate DATETIME,
	@Active BIT,
	@Id int output
)
AS
BEGIN

	INSERT INTO Products(Name, Description, Price, Quantity, RegistrationUser, RegistrationDate, Active)
	VALUES (@Name, @Description, @Price, @Quantity, @RegistrationUser, @RegistrationDate, @Active);
	SET @id=SCOPE_IDENTITY()
END;
GO
/****** Object:  StoredProcedure [dbo].[RegisterUser]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RegisterUser](
	@Email varchar(80),
	@Password varchar(150),
	@RegistrationDate datetime,
	@Active bit,
	@Id int output
)
AS
BEGIN

	INSERT INTO Users (Email, Password, RegistrationDate, Active) VALUES (@Email, @Password, @RegistrationDate, @Active);
	SET @id=SCOPE_IDENTITY()
END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateProduct]
(
	@Id INT,
	@Name VARCHAR(100),
	@Description VARCHAR(500),
	@Price DECIMAL(19,2),
	@Quantity INT,
	@ModificationUser VARCHAR(80),
	@ModificationDate DATETIME,
	@Active BIT
)
AS
BEGIN

	UPDATE Products
		SET Name = @Name,
			Description = @Description,
			Price = @Price,
			Quantity = @Quantity,
			Active = @Active,
			ModificationUser = @ModificationUser,
			ModificationDate = @ModificationDate
	 WHERE Id = @Id

END;
GO
/****** Object:  StoredProcedure [dbo].[ValidateProductExistence]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidateProductExistence]
(
	@Name VARCHAR(100)
)
AS
BEGIN
DECLARE @NameExists int

	SET @NameExists = (SELECT COUNT(Id) From Products Where UPPER(Name) = UPPER(@Name))
	SELECT @NameExists
END;
GO
/****** Object:  StoredProcedure [dbo].[ValidateUserExistence]    Script Date: 10/07/2023 01:59:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ValidateUserExistence]
(@Email VARCHAR(80))
AS
BEGIN
DECLARE @EmailExists int

	SET @EmailExists = (SELECT COUNT(Id) From Users Where UPPER(Email) = UPPER(@Email))
	SELECT @EmailExists
END;
GO
USE [master]
GO
ALTER DATABASE [exercisedb] SET  READ_WRITE 
GO
