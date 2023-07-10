using FluentValidation;

namespace Exercise.BizLogic.ViewModels.Product
{
    public class UpdateProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ModificationUser { get; set; }
        public bool Active { get; set; }
    }

    public class UpdateProducValidator : AbstractValidator<UpdateProductModel>
    {
        public UpdateProducValidator()
        {
            RuleFor(model => model.Id).GreaterThan(0);
            RuleFor(model => model.Name).NotNull().NotEmpty();
            RuleFor(model => model.Description).NotNull().NotEmpty();
            RuleFor(model => model.Price).NotNull().NotEmpty();
            RuleFor(model => model.Quantity).GreaterThan(0);
            RuleFor(model => model.ModificationUser).NotNull().NotEmpty();
        }
    }
}
