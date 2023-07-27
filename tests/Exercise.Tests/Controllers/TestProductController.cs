using Exercise.API.Controllers;
using Exercise.BizLogic.Products;
using Exercise.BizLogic.ViewModels.Product;
using Exercise.Tests.Fixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Exercise.Tests.Controllers
{
    public class TestProductController
    {
        private readonly Mock<IProductBizLogic> mockIProductBizLogic;
        private readonly Mock<IProductQueries> mockIProductQueries;
        private readonly Mock<ILogger<ProductController>> mockLogger;
        private readonly ProductController sut;

        public TestProductController()
        {
            mockIProductBizLogic = new Mock<IProductBizLogic>();
            mockIProductQueries = new Mock<IProductQueries>();
            mockLogger = new Mock<ILogger<ProductController>>();
            sut = new ProductController(mockIProductBizLogic.Object, mockIProductQueries.Object, mockLogger.Object);
        }

        [Fact]
        public async Task GetProducts_OnSuccess_ReturnStatusCode200()
        {
            //arrange

            //act
            var result = (ObjectResult)await sut.GetProducts();

            //assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetProducts_OnSuccess_InvokesProductQueriesExactlyOnce()
        {
            //arrange

            mockIProductQueries
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(Enumerable.Empty<GetProductModel>);

            var sut = new ProductController(mockIProductBizLogic.Object, mockIProductQueries.Object, mockLogger.Object);

            //act
            var result = (ObjectResult)await sut.GetProducts();

            //assert
            mockIProductQueries.Verify(bizlogic => bizlogic.GetAllProducts(), Times.Once);
        }

        [Fact]
        public async Task GetProducts_OnSuccess_ReturnListOfProducts()
        {
            //arrange

            mockIProductQueries
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(Enumerable.Empty<GetProductModel>);

            var sut = new ProductController(mockIProductBizLogic.Object, mockIProductQueries.Object, mockLogger.Object);

            //act
            var result = await sut.GetProducts();

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;

            //need to check why .BeOfType is not working for IEnumerable
            objectResult.Value.Should().BeAssignableTo<IEnumerable<GetProductModel>>();
        }


        [Fact]
        public async Task GetProductById_OnSuccess_ReturnStatusCode200()
        {
            //arrange

            mockIProductQueries
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(new GetProductByIdModel());

            var sut = new ProductController(mockIProductBizLogic.Object, mockIProductQueries.Object, mockLogger.Object);

            //act
            var result = (ObjectResult)await sut.GetProductById(1);

            //assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetProductById_OnSuccess_InvokesProductQueriesExactlyOnce()
        {
            //arrange

            mockIProductQueries
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(new GetProductByIdModel());

            var sut = new ProductController(mockIProductBizLogic.Object, mockIProductQueries.Object, mockLogger.Object);

            //act
            var result = (ObjectResult)await sut.GetProductById(1);

            //assert
            mockIProductQueries.Verify(bizlogic => bizlogic.GetProductById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetProductById_OnSuccess_ReturnUser()
        {
            //arrange

            mockIProductQueries
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(new GetProductByIdModel());

            var sut = new ProductController(mockIProductBizLogic.Object, mockIProductQueries.Object, mockLogger.Object);

            //act
            var result = await sut.GetProductById(1);

            //assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<GetProductByIdModel>();
        }

        [Fact]
        public async Task GetProductById_OnProductNotFound_Return404()
        {
            //arrange
            GetProductByIdModel entity = null;

            mockIProductQueries
                .Setup(x => x.GetProductById(It.IsAny<int>())).ReturnsAsync(entity);

            var sut = new ProductController(mockIProductBizLogic.Object, mockIProductQueries.Object, mockLogger.Object);

            //act
            var result = await sut.GetProductById(1);

            //assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task CreateProduct_OnSuccess_ReturnStatusCode201()
        {
            //arrange
            var createProductModel = ProductFixture.GetSingleCreateProduct();

            mockIProductBizLogic
                .Setup(x => x.CreateProduct(It.IsAny<CreateProductModel>())).ReturnsAsync(1);

            var sut = new ProductController(mockIProductBizLogic.Object, mockIProductQueries.Object, mockLogger.Object);

            //act
            var result = (CreatedAtActionResult)await sut.CreateProduct(createProductModel);

            //assert
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task CreateProduct_OnValidation_ReturnValidationMessageForProperties()
        {
            //arrange
            CreateProducValidator validator = new();
            var model = ProductFixture.GetSingleCreateProductWithErrors();

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
            result.ShouldHaveValidationErrorFor(x => x.Price);
            result.ShouldHaveValidationErrorFor(x => x.RegistrationUser);
        }


        [Fact]
        public async Task UpdateProduct_OnSuccess_ReturnStatusCode200()
        {
            //arrange
            var model = ProductFixture.GetSingleUpdateProduct();

            mockIProductBizLogic
                .Setup(x => x.UpdateProduct(It.IsAny<UpdateProductModel>())).ReturnsAsync(1);

            var sut = new ProductController(mockIProductBizLogic.Object, mockIProductQueries.Object, mockLogger.Object);

            //act
            var result = (OkResult)await sut.UpdateProduct(model);

            //assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task UpdateProduct_OnValidation_ReturnValidationMessageForProperties()
        {
            //arrange
            UpdateProducValidator validator = new();
            var model = ProductFixture.GetSingleUpdateProductWithErrors();

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.Name);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.Quantity);
            result.ShouldHaveValidationErrorFor(x => x.Price);
            result.ShouldHaveValidationErrorFor(x => x.ModificationUser);
        }

        [Fact]
        public async Task DeleteProduct_OnSuccess_ReturnStatusCode200()
        {
            //arrange
            var model = ProductFixture.GetSingleDeleteProduct();

            mockIProductBizLogic
                .Setup(x => x.DeleteProduct(It.IsAny<DeleteProductModel>())).ReturnsAsync(1);

            var sut = new ProductController(mockIProductBizLogic.Object, mockIProductQueries.Object, mockLogger.Object);

            //act
            var result = (OkResult)await sut.DeleteProduct(model);

            //assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DeleteProduct_OnValidation_ReturnValidationMessageForProperties()
        {
            //arrange
            DeleteProducValidator validator = new();
            var model = ProductFixture.GetSingleDeleteWithErrors();

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Id);
            result.ShouldHaveValidationErrorFor(x => x.ModificationUser);
        }
    }
}
