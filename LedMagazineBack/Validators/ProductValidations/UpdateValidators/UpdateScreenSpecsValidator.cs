using FluentValidation;
using LedMagazineBack.Models.ProductModels.UpdateModels;

namespace LedMagazineBack.Validators.ProductValidations.UpdateValidators;

public class UpdateScreenSpecsValidator : AbstractValidator<UpdateScreenSpecsModel>
{
    public UpdateScreenSpecsValidator()
    {
        When(x => x.ScreenResolution is not null, () =>
        {
            RuleFor(x => x.ScreenSize)
                .NotEmpty().WithMessage("ScreenSize cannot be empty")
                .MaximumLength(15).WithMessage("ScreenSize cannot be less than 15 characters");
        });

        When(x => x.ScreenType is not null, () =>
        {
            RuleFor(x => x.ScreenType)
                .NotEmpty().WithMessage("ScreenType cannot be empty")
                .MaximumLength(15).WithMessage("ScreenType cannot be less than 15 characters");
        });

        When(x => x.ScreenResolution is not null, () =>
        {
            RuleFor(x => x.ScreenResolution)
                .NotEmpty().WithMessage("ScreenResolution cannot be empty")
                .MaximumLength(15).WithMessage("ScreenResolution cannot be less than 15 characters");
        });
    }
}