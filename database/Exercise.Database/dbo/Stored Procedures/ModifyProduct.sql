CREATE PROCEDURE ModifyProduct
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