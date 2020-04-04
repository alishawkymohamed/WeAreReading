using FluentValidation;
using Models.DTOs;

namespace WeAreReading.Validators
{
    public class BookDtoValidator : AbstractValidator<BookDTO>
    {
        public BookDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull();
            RuleFor(x => x.Title).MinimumLength(5);
            RuleFor(x => x.Author).NotEmpty().NotNull();
            RuleFor(x => x.Author).MinimumLength(5);
            RuleFor(x => x.CategoryId).NotEmpty().NotNull();
            RuleFor(x => x.CopiesCount).NotEmpty().NotNull();
            RuleFor(x => x.Description).NotEmpty().NotNull();
            RuleFor(x => x.Description).MinimumLength(5);
            RuleFor(x => x.CoverPhotoId).NotEmpty().NotNull();
        }
    }
}
