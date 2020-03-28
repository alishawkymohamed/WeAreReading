using FluentValidation;
using Models.DTOs;

namespace WeAreReading.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Password).NotEmpty().NotNull();
            RuleFor(x => x.Username).NotEmpty().NotNull();
        }
    }
}
