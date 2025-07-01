using FluentValidation;
using LedMagazineBack.Models.ProductModels.UpdateModels;

namespace LedMagazineBack.Validators.ProductValidations.UpdateValidators;

public class UpdateProductGenInfoValidator : AbstractValidator<UpdateProductGenInfoModel>
{
    public UpdateProductGenInfoValidator()
    {
        When(x => x.Name is not null, () =>
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is empty")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
        });
        When(x => x.Description is not null, () =>
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required")
                .MaximumLength(10000).WithMessage("Description must not exceed 10000 characters");
        });
        When(x => x.Duration is not null, () =>
        {
            RuleFor(x => x.Duration)
                .NotEmpty().WithMessage("Duration is required")
                .NotNull().WithMessage("Duration is required")
                .MaximumLength(20).WithMessage("Duration must not exceed 20 characters");
        });
    }
}