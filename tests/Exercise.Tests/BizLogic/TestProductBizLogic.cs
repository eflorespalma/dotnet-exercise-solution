using Exercise.BizLogic.Products;
using Exercise.Domain;
using Exercise.Domain.Exceptions;
using Exercise.Repository.Interfaces;
using Exercise.Repository.UnitOfWork;
using Exercise.Tests.Fixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Exercise.Tests.BizLogic
{
    public class TestProductBizLogic
    {
        private readonly Mock<IUnitofWork> mockUnitOfWork;
        private readonly Mock<ILogger<ProductBizLogic>> mockLogger;
        private readonly ProductBizLogic productBizLogic;

        public TestProductBizLogic()
        {
            mockUnitOfWork = new Mock<IUnitofWork>();
            mockLogger = new Mock<ILogger<ProductBizLogic>>();
            productBizLogic = new ProductBizLogic(mockUnitOfWork.Object, mockLogger.Object);
        }

        [Fact]
        public async Task CreateProduct_ValidModel_ReturnsId()
        {
            // Arrange
            var createProductModel = ProductFixture.GetSingleCreateProduct();
            mockUnitOfWork.Setup(uow => uow._productRepository.ValidateProductExistence(It.IsAny<string>())).ReturnsAsync(false);
            mockUnitOfWork.Setup(uow => uow._productRepository.CreateProduct(It.IsAny<Product>())).ReturnsAsync(1);

            // Act
            var result = await productBizLogic.CreateProduct(createProductModel);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task CreateProduct_ProductNameExists_ThrowsException()
        {
            // Arrange
            var createProductModel = ProductFixture.GetSingleCreateProduct();
            mockUnitOfWork.Setup(uow => uow._productRepository.ValidateProductExistence(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            Func<Task> action = async () => await productBizLogic.CreateProduct(createProductModel);

            // Assert
            await action.Should().ThrowAsync<ExerciseBaseException>().WithMessage($"Product {createProductModel.Name} already exists in database.");
        }

        [Fact]
        public async Task CreateProduct_OnSuccess_VerifyCommitInvokationOnce()
        {
            //arrange
            var createProductModel = ProductFixture.GetSingleCreateProduct();
            mockUnitOfWork.Setup(uow => uow._productRepository.ValidateProductExistence(It.IsAny<string>())).ReturnsAsync(false);
            mockUnitOfWork.Setup(uow => uow._productRepository.CreateProduct(It.IsAny<Product>())).ReturnsAsync(1);

            // Act
            var result = await productBizLogic.CreateProduct(createProductModel);

            // Assert
            mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public async Task CreateProduct_ExistingProduct_RollsBackTransaction()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            mockUnitOfWork.Setup(uow => uow._productRepository).Returns(productRepositoryMock.Object);

            var createProductModel = ProductFixture.GetSingleCreateProductWithErrors();

            productRepositoryMock.Setup(repo => repo.ValidateProductExistence(createProductModel.Name)).ReturnsAsync(true);

            // Act
            Func<Task> act = async () => await productBizLogic.CreateProduct(createProductModel);

            // Assert
            await act.Should().ThrowAsync<ExerciseBaseException>().WithMessage($"Product {createProductModel.Name} already exists in database.");
            mockUnitOfWork.Verify(uow => uow.Commit(), Times.Never);
            mockUnitOfWork.Verify(uow => uow.RollBack(), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_ValidModel_ReturnsRowsAffected()
        {
            // Arrange
            var updateProductModel = ProductFixture.GetSingleUpdateProduct();
            var updateOldProductModel = ProductFixture.GetProductEntity();

            mockUnitOfWork.Setup(uow => uow._productRepository.ValidateProductExistence(It.IsAny<string>())).ReturnsAsync(false);
            mockUnitOfWork.Setup(uow => uow._productRepository.UpdateProduct(It.IsAny<Product>())).ReturnsAsync(1);
            mockUnitOfWork.Setup(uow => uow._productRepository.GetProductById(It.IsAny<int>())).ReturnsAsync(updateOldProductModel);

            // Act
            var result = await productBizLogic.UpdateProduct(updateProductModel);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task UpdateProduct_ProductNameExists_ThrowsException()
        {
            // Arrange
            var updateProductModel = ProductFixture.GetSingleUpdateProduct();
            var updateOldProductModel = ProductFixture.GetErrorProductEntity();

            mockUnitOfWork.Setup(uow => uow._productRepository.ValidateProductExistence(It.IsAny<string>())).ReturnsAsync(true);
            mockUnitOfWork.Setup(uow => uow._productRepository.GetProductById(It.IsAny<int>())).ReturnsAsync(updateOldProductModel);

            // Act
            Func<Task> action = async () => await productBizLogic.UpdateProduct(updateProductModel);

            // Assert
            await action.Should().ThrowAsync<ExerciseBaseException>().WithMessage($"Product {updateProductModel.Name} already exists in database.");
        }

        [Fact]
        public async Task UpdateProduct_OnSuccess_VerifyCommitInvokationOnce()
        {
            //arrange
            var updateProductModel = ProductFixture.GetSingleUpdateProduct();
            var updateOldProductModel = ProductFixture.GetErrorProductEntity();
            mockUnitOfWork.Setup(uow => uow._productRepository.ValidateProductExistence(It.IsAny<string>())).ReturnsAsync(false);
            mockUnitOfWork.Setup(uow => uow._productRepository.UpdateProduct(It.IsAny<Product>())).ReturnsAsync(1);
            mockUnitOfWork.Setup(uow => uow._productRepository.GetProductById(It.IsAny<int>())).ReturnsAsync(updateOldProductModel);

            // Act
            var result = await productBizLogic.UpdateProduct(updateProductModel);

            // Assert
            mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_ExistingEmail_RollsBackTransaction()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var updateOldProductModel = ProductFixture.GetErrorProductEntity();
            mockUnitOfWork.Setup(uow => uow._productRepository).Returns(productRepositoryMock.Object);

            var updateProductModel = ProductFixture.GetSingleUpdateProductWithErrors();

            productRepositoryMock.Setup(repo => repo.ValidateProductExistence(updateProductModel.Name)).ReturnsAsync(true);
            mockUnitOfWork.Setup(uow => uow._productRepository.GetProductById(It.IsAny<int>())).ReturnsAsync(updateOldProductModel);

            // Act
            Func<Task> act = async () => await productBizLogic.UpdateProduct(updateProductModel);

            // Assert
            await act.Should().ThrowAsync<ExerciseBaseException>().WithMessage($"Product {updateProductModel.Name} already exists in database.");
            mockUnitOfWork.Verify(uow => uow.Commit(), Times.Never);
            mockUnitOfWork.Verify(uow => uow.RollBack(), Times.Once);
        }

        [Fact]
        public async Task DeleteProduct_ValidModel_ReturnsRowsAffected()
        {
            // Arrange
            var deleteProductModel = ProductFixture.GetSingleDeleteProduct();
            mockUnitOfWork.Setup(uow => uow._productRepository.ValidateProductExistence(It.IsAny<string>())).ReturnsAsync(false);
            mockUnitOfWork.Setup(uow => uow._productRepository.DeleteProduct(It.IsAny<Product>())).ReturnsAsync(1);

            // Act
            var result = await productBizLogic.DeleteProduct(deleteProductModel);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task DeleteProduct_OnSuccess_VerifyCommitInvokationOnce()
        {
            //arrange
            var deleteProductModel = ProductFixture.GetSingleDeleteProduct();
            mockUnitOfWork.Setup(uow => uow._productRepository.DeleteProduct(It.IsAny<Product>())).ReturnsAsync(1);

            // Act
            var result = await productBizLogic.DeleteProduct(deleteProductModel);

            // Assert
            mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }
    }
}
