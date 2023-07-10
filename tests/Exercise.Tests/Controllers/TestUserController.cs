using Exercise.API.Controllers;
using Exercise.BizLogic.Users;
using Exercise.BizLogic.ViewModels.User;
using Exercise.Tests.Fixture;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Exercise.Tests.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task CreateUser_OnSuccess_ReturnStatusCode201()
        {
            //arrange
            var mockIUsertBizLogic = new Mock<IUserBizLogic>();
            var mockLogger = new Mock<ILogger<UserController>>();
            var createUserModel = UserFixture.GetSingleCreateUser();

            mockIUsertBizLogic
                .Setup(x => x.CreateUser(It.IsAny<CreateUserModel>())).ReturnsAsync(1);

            var sut = new UserController(mockLogger.Object, mockIUsertBizLogic.Object);

            //act
            var result = (CreatedAtActionResult)await sut.CreateUser(createUserModel);

            //assert
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task CreateProduct_OnValidation_ReturnValidationMessageForProperties()
        {
            //arrange
            CreateUserModelValidator validator = new();
            var model = UserFixture.GetSingleCreateUserWithErrors();

            //act
            var result = await validator.TestValidateAsync(model);

            //assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }
    }
}
