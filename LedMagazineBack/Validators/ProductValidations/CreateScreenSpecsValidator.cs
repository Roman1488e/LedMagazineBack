using FluentValidation;
using LedMagazineBack.Models.ProductModels.CreationModels;

namespace LedMagazineBack.Validators.ProductValidations;

public class CreateScreenSpecsValidator : AbstractValidator<CreateScreenSpecsModel>
{
    public CreateScreenSpecsValidator()
    {
        RuleFor(x=>x.ScreenSize)
            .NotNull().WithMessage("ScreenSize cannot be empty")
            .NotEmpty().WithMessage("ScreenSize cannot be empty")
            .MaximumLength(15).WithMessage("ScreenSize cannot be less than 15 characters");
        
        RuleFor(x=> x.ScreenType)
            .NotNull().WithMessage("ScreenType cannot be empty")
            .NotEmpty().WithMessage("ScreenType cannot be empty")
            .MaximumLength(15).WithMessage("ScreenType cannot be less than 15 characters");
        
        RuleFor(x=> x.ScreenResolution)
            .NotNull().WithMessage("ScreenResolution cannot be empty")
            .NotEmpty().WithMessage("ScreenResolution cannot be empty")
            .MaximumLength(15).WithMessage("ScreenResolution cannot be less than 15 characters");
        
        RuleFor(x=> x.ProductId)
            .NotEqual(Guid.Empty).WithMessage("ProductId cannot be empty");
    }
}