
CREATE PROCEDURE ValidateUserExistence
(@Email VARCHAR(80))
AS
BEGIN
DECLARE @EmailExists int

	SET @EmailExists = (SELECT COUNT(Id) From Users Where UPPER(Email) = UPPER(@Email))
	SELECT @EmailExists
END;