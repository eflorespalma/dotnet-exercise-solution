CREATE PROCEDURE UpdateProduct
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