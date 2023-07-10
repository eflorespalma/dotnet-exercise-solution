using Exercise.BizLogic.ViewModels.Product;
using Exercise.Domain;

namespace Exercise.Tests.Fixture
{
    public static class ProductFixture
    {
        public static CreateProductModel GetSingleCreateProduct()
        {
            return new CreateProductModel()
            {
                Name = "Skullcandy Hesh Evo",
                Description = "Supreme Audio with powerful 40mm drivers and exceptional acoustics your favorite tunes will be crystal clear",
                Price = 50,
                Quantity = 2,
                RegistrationUser = "eflorespalma@gmail.com"
            };
        }

        public static CreateProductModel GetSingleCreateProductWithErrors()
        {
            return new CreateProductModel()
            {
                Name = string.Empty,
                Description = string.Empty,
                Price = 0,
                Quantity = 0,
                RegistrationUser = string.Empty
            };
        }

        public static UpdateProductModel GetSingleUpdateProduct()
        {
            return new UpdateProductModel()
            {
                Id = 1,
                Name = "Skullcandy Hesh Evo",
                Description = "Supreme Audio with powerful 40mm drivers and exceptional acoustics your favorite tunes will be crystal clear",
                Price = 50,
                Quantity = 3,
                ModificationUser = "eflorespalma@gmail.com"
            };
        }

        public static Product GetProductEntity()
        {
            var product = new Product(1, "Skullcandy Hesh Evo", "Supreme Audio with powerful 40mm drivers and exceptional acoustics your favorite tunes will be crystal clear", 50, 3, "eflorespalma@gmail.com", true);

            return product;
        }

        public static Product GetErrorProductEntity()
        {
            var product = new Product(1, "Skullcandy Hesh Evo1", "Supreme Audio with powerful 40mm drivers and exceptional acoustics your favorite tunes will be crystal clear", 50, 3, "eflorespalma@gmail.com", true);

            return product;
        }
        public static UpdateProductModel GetSingleUpdateProductWithErrors()
        {
            return new UpdateProductModel()
            {
                Id = 0,
                Name = string.Empty,
                Description = string.Empty,
                Price = 0,
                Quantity = 0,
                ModificationUser = string.Empty
            };
        }

        public static DeleteProductModel GetSingleDeleteProduct()
        {
            return new DeleteProductModel()
            {
                Id = 1,
                ModificationUser = "eflorespalma@gmail.com"
            };
        }

        public static DeleteProductModel GetSingleDeleteWithErrors()
        {
            return new DeleteProductModel()
            {
                Id = 0,
                ModificationUser = string.Empty
            };
        }
    }
}
