using Exercise.Domain;
using Exercise.Tests.Fixture;
using FluentAssertions;
using System;
using Xunit;

namespace Exercise.Tests.Domain
{
    public class TestProductDomain
    {
        [Fact]
        public void Constructor_WithParameters_SetsProperties()
        {
            // Arrange
            var createProductModel = ProductFixture.GetSingleCreateProduct();

            // Act
            var product = new Product(createProductModel.Name, createProductModel.Description, createProductModel.Price, createProductModel.Quantity, createProductModel.RegistrationUser);

            // Assert
            product.Name.Should().Be(createProductModel.Name);
            product.Description.Should().Be(createProductModel.Description);
            product.Price.Should().Be(createProductModel.Price);
            product.Quantity.Should().Be(createProductModel.Quantity);
            product.RegistrationUser.Should().Be(createProductModel.RegistrationUser);
            product.RegistrationDate.Should().BeCloseTo(DateTime.Now, precision: TimeSpan.FromSeconds(1));
            product.Active.Should().BeTrue();
        }

        [Fact]
        public void Constructor_WithIdAndModificationUser_SetsProperties()
        {
            // Arrange
            var updateProductModel = ProductFixture.GetSingleUpdateProduct();

            // Act
            var product = new Product(updateProductModel.Id, updateProductModel.Name, updateProductModel.Description, updateProductModel.Price, updateProductModel.Quantity, updateProductModel.ModificationUser, updateProductModel.Active);

            // Assert
            product.Id.Should().Be(updateProductModel.Id);
            product.Name.Should().Be(updateProductModel.Name);
            product.Description.Should().Be(updateProductModel.Description);
            product.Price.Should().Be(updateProductModel.Price);
            product.Quantity.Should().Be(updateProductModel.Quantity);
            product.ModificationUser.Should().Be(updateProductModel.ModificationUser);
            product.ModificationDate.Should().BeCloseTo(DateTime.Now, precision: TimeSpan.FromSeconds(1));
            product.Active.Should().Be(updateProductModel.Active);
        }

        [Fact]
        public void Constructor_WithIdAndModificationUser_SetsActiveToFalse()
        {
            // Arrange
            var deleteProductModel = ProductFixture.GetSingleDeleteProduct();


            // Act
            var product = new Product(deleteProductModel.Id, deleteProductModel.ModificationUser);

            // Assert
            product.Id.Should().Be(deleteProductModel.Id);
            product.ModificationUser.Should().Be(deleteProductModel.ModificationUser);
            product.ModificationDate.Should().BeCloseTo(DateTime.Now, precision: TimeSpan.FromSeconds(1));
            product.Active.Should().BeFalse();
        }
    }
}
