CREATE PROCEDURE RegisterUser(
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