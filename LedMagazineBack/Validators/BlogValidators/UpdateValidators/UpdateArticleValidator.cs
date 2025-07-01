using FluentValidation;
using LedMagazineBack.Models.BlogModels.UpdateModels;

namespace LedMagazineBack.Validators.BlogValidators.UpdateValidators;

public class UpdateArticleValidator : AbstractValidator<UpdateArticleModel>
{
    public UpdateArticleValidator()
    {
        When(x => x.Title != null, () =>
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title cannot be empty")
                .NotEmpty().WithMessage("Title cannot be empty")
                .MaximumLength(30).WithMessage("Title cannot be less than 30 characters");
        });

        When(x => x.Content is not null, () =>
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content cannot be empty")
                .MaximumLength(20000).WithMessage("Content cannot be less than 20000 characters");
        });
    }
}