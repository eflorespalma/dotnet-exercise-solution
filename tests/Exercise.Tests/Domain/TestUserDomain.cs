using Exercise.Domain;
using Exercise.Tests.Fixture;
using FluentAssertions;
using System;
using Xunit;

namespace Exercise.Tests.Domain
{
    public class TestUserDomain
    {
        [Fact]
        public void Constructor_WithParameters_SetsProperties()
        {
            // Arrange
            var createUserModel = UserFixture.GetSingleCreateUser();

            // Act
            var user = new User(createUserModel.Email, createUserModel.Password);

            // Assert
            user.Email.Should().Be(createUserModel.Email);
            user.Password.Should().Be(createUserModel.Password);
            user.RegistrationDate.Should().BeCloseTo(DateTime.Now, precision: TimeSpan.FromSeconds(1));
            user.Active.Should().BeTrue();
        }

        [Fact]
        public void Constructor_Default_SetsPropertiesToDefaultValues()
        {
            // Act
            var user = new User();

            // Assert
            user.Email.Should().BeNull();
            user.Password.Should().BeNull();
            user.RegistrationDate.Should().Be(default(DateTime));
            user.Active.Should().BeFalse();
        }
    }
}
