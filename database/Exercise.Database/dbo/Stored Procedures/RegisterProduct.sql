CREATE PROCEDURE RegisterProduct
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