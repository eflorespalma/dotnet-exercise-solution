CREATE PROCEDURE ValidateProductExistence
(
	@Name VARCHAR(100)
)
AS
BEGIN
DECLARE @NameExists int

	SET @NameExists = (SELECT COUNT(Id) From Products Where UPPER(Name) = UPPER(@Name))
	SELECT @NameExists
END;