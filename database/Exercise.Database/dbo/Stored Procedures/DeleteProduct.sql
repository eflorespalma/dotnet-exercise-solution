CREATE PROCEDURE DeleteProduct
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