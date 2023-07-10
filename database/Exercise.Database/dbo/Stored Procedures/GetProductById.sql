CREATE PROCEDURE GetProductById
(
	@Id INT
)
AS
BEGIN
	SELECT Name, Description, Price, Quantity, Active FROM Products
	WHERE Id = @Id
END;