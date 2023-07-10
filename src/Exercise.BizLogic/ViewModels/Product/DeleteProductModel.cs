using FluentValidation;

namespace Exercise.BizLogic.ViewModels.Product
{

    public class DeleteProductModel
    {
        public int Id { get; set; }
        public string ModificationUser { get; set; }
    }

    public class DeleteProducValidator : AbstractValidator<DeleteProductModel>
    {
        public DeleteProducValidator()
        {
            RuleFor(model => model.Id).GreaterThan(0);
            RuleFor(model => model.ModificationUser).NotNull().NotEmpty();
        }
    }
}
