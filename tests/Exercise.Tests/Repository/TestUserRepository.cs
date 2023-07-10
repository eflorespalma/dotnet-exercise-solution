using Dapper;
using Exercise.Repository.Core;
using Exercise.Repository.Users;
using Exercise.Tests.Fixture;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Exercise.Tests.Repository
{
    public class TestUserRepository
    {
        [Fact]
        public async Task CreateUser_ShouldCallExecuteAsyncWithCorrectParameters()
        {
            // Arrange
            var mockDbService = new Mock<IDbService>();
            var userRepository = new UserRepository(mockDbService.Object);
            var user = UserFixture.GetUserEntity();

            // Act
            await userRepository.CreateUser(user);

            // Assert
            mockDbService.Verify(
                dbService => dbService.ExecuteAsync("[dbo].[RegisterUser]", It.IsAny<DynamicParameters>()),
                Times.Once);
        }
    }
}
