using Exercise.BizLogic.Users;
using Exercise.Domain;
using Exercise.Domain.Exceptions;
using Exercise.Repository.UnitOfWork;
using Exercise.Repository.Users;
using Exercise.Tests.Fixture;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Exercise.Tests.BizLogic
{
    public class TestUserBizLogic
    {
        private readonly Mock<IUnitofWork> mockUnitOfWork;
        private readonly UserBizLogic userBizLogic;

        public TestUserBizLogic()
        {
            mockUnitOfWork = new Mock<IUnitofWork>();
            userBizLogic = new UserBizLogic(mockUnitOfWork.Object);
        }

        [Fact]
        public async Task CreateProduct_ValidModel_ReturnsId()
        {
            // Arrange
            var createUserModel = UserFixture.GetSingleCreateUser();
            mockUnitOfWork.Setup(uow => uow._userRepository.ValidateUserExistence(It.IsAny<string>())).ReturnsAsync(false);
            mockUnitOfWork.Setup(uow => uow._userRepository.CreateUser(It.IsAny<User>())).ReturnsAsync(1);

            // Act
            var result = await userBizLogic.CreateUser(createUserModel);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task CreateUser_ProductNameExists_ThrowsException()
        {
            // Arrange
            var createUserModel = UserFixture.GetSingleCreateUser();
            mockUnitOfWork.Setup(uow => uow._userRepository.ValidateUserExistence(It.IsAny<string>())).ReturnsAsync(true);

            // Act
            Func<Task> action = async () => await userBizLogic.CreateUser(createUserModel);

            // Assert
            await action.Should().ThrowAsync<ExerciseBaseException>().WithMessage($"Email {createUserModel.Email} already exists in database.");
        }

        [Fact]
        public async Task CreateUser_OnSuccess_VerifyCommitInvokationOnce()
        {
            //arrange
            var createUserModel = UserFixture.GetSingleCreateUser();
            mockUnitOfWork.Setup(uow => uow._userRepository.ValidateUserExistence(It.IsAny<string>())).ReturnsAsync(false);
            mockUnitOfWork.Setup(uow => uow._userRepository.CreateUser(It.IsAny<User>())).ReturnsAsync(1);

            // Act
            var result = await userBizLogic.CreateUser(createUserModel);

            // Assert
            mockUnitOfWork.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public async Task CreateUser_ExistingEmail_RollsBackTransaction()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            mockUnitOfWork.Setup(uow => uow._userRepository).Returns(userRepositoryMock.Object);

            var createUserModel = UserFixture.GetSingleCreateUser();

            userRepositoryMock.Setup(repo => repo.ValidateUserExistence(createUserModel.Email)).ReturnsAsync(true);

            var userBizLogic = new UserBizLogic(mockUnitOfWork.Object);

            // Act
            Func<Task> act = async () => await userBizLogic.CreateUser(createUserModel);

            // Assert
            await act.Should().ThrowAsync<ExerciseBaseException>().WithMessage($"Email {createUserModel.Email} already exists in database.");
            mockUnitOfWork.Verify(uow => uow.Commit(), Times.Never);
            mockUnitOfWork.Verify(uow => uow.RollBack(), Times.Once);
        }
    }
}
