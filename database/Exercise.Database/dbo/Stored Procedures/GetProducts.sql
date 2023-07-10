CREATE PROCEDURE GetProducts
AS
BEGIN
	SELECT Id, Name, Description, Price, Quantity, Active FROM Products
END;