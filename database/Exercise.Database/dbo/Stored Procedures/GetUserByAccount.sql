CREATE PROCEDURE GetUserByAccount(
	@Email VARCHAR(80)
)
AS
BEGIN

SELECT Email, Password, Active FROM Users where Email = @Email
	
END;