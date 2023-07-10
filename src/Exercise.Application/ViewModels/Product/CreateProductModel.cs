using FluentValidation;

namespace Exercise.BizLogic.ViewModels.Product
{
    public class CreateProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string RegistrationUser { get; set; }
    }

    public class CreateProducValidator : AbstractValidator<CreateProductModel>
    {
        public CreateProducValidator()
        {
            RuleFor(model => model.Name).NotNull().NotEmpty();
            RuleFor(model => model.Description).NotNull().NotEmpty();
            RuleFor(model => model.Price).GreaterThan(0);
            RuleFor(model => model.Quantity).GreaterThan(0);
            RuleFor(model => model.RegistrationUser).NotNull().NotEmpty();
        }
    }
}
