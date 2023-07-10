using Exercise.BizLogic.ViewModels.Product;
using FluentValidation;

namespace Exercise.BizLogic.ViewModels.User
{
    public class CreateUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUserModelValidator()
        {
            RuleFor(model => model.Email).NotNull().NotEmpty()?.EmailAddress();
            RuleFor(model => model.Password).NotNull().NotEmpty();
        }
    }
}
