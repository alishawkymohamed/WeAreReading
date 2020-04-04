using FluentValidation;
using Models.DTOs;

namespace WeAreReading.Validators
{
    public class UserRegisterValidator : AbstractValidator<RegisterUserDTO>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty();
            RuleFor(x => x.FullName).NotNull().NotEmpty();
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password)
                .WithMessage(x => $"'{nameof(x.ConfirmPassword)}' must be equal '{nameof(x.Password)}'");
            RuleFor(x => x.Username).NotNull().NotEmpty();
            RuleFor(x => x.RoleId).NotNull().NotEmpty();
        }
    }
}
