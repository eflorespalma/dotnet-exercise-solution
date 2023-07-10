using Dapper;
using Exercise.Domain;
using Exercise.Repository.Core;
using Exercise.Repository.Products;
using Exercise.Tests.Fixture;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Exercise.Tests.Repository
{
    public class TestProductRepository
    {
        [Fact]
        public async Task CreateProduct_ShouldCallExecuteAsyncWithCorrectParameters()
        {
            // Arrange
            var mockDbService = new Mock<IDbService>();
            var productRepository = new ProductRepository(mockDbService.Object);
            var product = ProductFixture.GetProductEntity();

            // Act
            await productRepository.CreateProduct(product);

            // Assert
            mockDbService.Verify(
                dbService => dbService.ExecuteAsync("[dbo].[RegisterProduct]", It.IsAny<DynamicParameters>()),
                Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_ShouldCallExecuteAsyncWithCorrectParameters()
        {
            // Arrange
            var mockDbService = new Mock<IDbService>();
            var productRepository = new ProductRepository(mockDbService.Object);
            var product = ProductFixture.GetProductEntity();

            // Act
            await productRepository.UpdateProduct(product);

            // Assert
            mockDbService.Verify(
                dbService => dbService.ExecuteAsync("[dbo].[UpdateProduct]", It.IsAny<DynamicParameters>()),
                Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ShouldCallExecuteAsyncWithCorrectParameters()
        {
            // Arrange
            var mockDbService = new Mock<IDbService>();
            var productRepository = new ProductRepository(mockDbService.Object);
            var product = ProductFixture.GetProductEntity();

            // Act
            await productRepository.DeleteProduct(product);

            // Assert
            mockDbService.Verify(
                dbService => dbService.ExecuteAsync("[dbo].[DeleteProduct]", It.IsAny<DynamicParameters>()),
                Times.Once);
        }

        [Fact]
        public async Task ValidateProductExistence_ShouldCallExecuteAsyncWithCorrectParameters()
        {
            // Arrange
            var mockDbService = new Mock<IDbService>();
            var productRepository = new ProductRepository(mockDbService.Object);
            var name = "Adidas";

            // Act
            await productRepository.ValidateProductExistence(name);

            // Assert
            mockDbService.Verify(
                dbService => dbService.ExecuteScalarAsync<bool>("[dbo].[ValidateProductExistence]", It.IsAny<DynamicParameters>()),
                Times.Once);
        }

        [Fact]
        public async Task GetProductById_ShouldCallExecuteAsyncWithCorrectParameters()
        {
            // Arrange
            var mockDbService = new Mock<IDbService>();
            var productRepository = new ProductRepository(mockDbService.Object);
            var id = 1;

            // Act
            await productRepository.GetProductById(id);

            // Assert
            mockDbService.Verify(
                dbService => dbService.QueryFirstOrDefaultAsync<Product>("[dbo].[GetProductById]", It.IsAny<DynamicParameters>()),
                Times.Once);
        }
    }
}
