using Exercise.Repository.Interfaces;
using Exercise.Repository.UnitOfWork;
using Exercise.Repository.Users;
using Moq;
using System.Data;
using Xunit;

namespace Exercise.Tests.Repository.UnitOfWork
{
    public class TestUnitOfWork
    {
        private readonly Mock<IDbTransaction> dbTransactionMock;
        private readonly Mock<IProductRepository> productRepositoryMock;
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly UnitofWork unitOfWork;

        public TestUnitOfWork()
        {
            dbTransactionMock = new Mock<IDbTransaction>();
            productRepositoryMock = new Mock<IProductRepository>();
            userRepositoryMock = new Mock<IUserRepository>();
            unitOfWork = new UnitofWork(dbTransactionMock.Object, productRepositoryMock.Object, userRepositoryMock.Object);
        }

        [Fact]
        public void Commit_Should_CommitTransaction()
        {
            // Arrange
            var unitOfWork = new UnitofWork(dbTransactionMock.Object, productRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            unitOfWork.Commit();

            // Assert
            dbTransactionMock.Verify(t => t.Commit(), Times.Once);
        }

        [Fact]
        public void RollBack_Should_RollbackTransaction()
        {
            // Arrange
            var unitOfWork = new UnitofWork(dbTransactionMock.Object, productRepositoryMock.Object, userRepositoryMock.Object);

            // Act
            unitOfWork.RollBack();

            // Assert
            dbTransactionMock.Verify(t => t.Rollback(), Times.Once);
        }

        [Fact]
        public void Dispose_Should_CloseConnectionAndDisposeTransaction()
        {
            // Arrange
            var connectionMock = new Mock<IDbConnection>();
            dbTransactionMock.SetupGet(t => t.Connection).Returns(connectionMock.Object);

            // Act
            unitOfWork.Dispose();

            // Assert
            connectionMock.Verify(c => c.Close(), Times.Once);
            connectionMock.Verify(c => c.Dispose(), Times.Once);
            dbTransactionMock.Verify(t => t.Dispose(), Times.Once);
        }
    }
}
