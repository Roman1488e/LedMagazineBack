using FluentValidation;
using LedMagazineBack.Models.BlogModels.CreationModels;

namespace LedMagazineBack.Validators.BlogValidators;

public class CreateArticleValidator : AbstractValidator<CreateArticleModel>
{
    public CreateArticleValidator()
    {
        RuleFor(x => x.Title)
            .NotNull().WithMessage("Title cannot be empty")
            .NotEmpty().WithMessage("Title cannot be empty")
            .MaximumLength(30).WithMessage("Title cannot be less than 30 characters");

        RuleFor(x=> x.Content)
            .NotNull().WithMessage("Content cannot be empty")
            .NotEmpty().WithMessage("Content cannot be empty")
            .MaximumLength(20000).WithMessage("Content cannot be less than 20000 characters");

        RuleFor(x => x.Image)
            .NotNull().WithMessage("Image cannot be empty");
        RuleFor(x => x.BlogId)
            .NotNull().WithMessage("BlogId cannot be empty")
            .NotEqual(Guid.Empty).WithMessage("BlogId cannot be empty");
    }
}