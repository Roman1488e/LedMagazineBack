using FluentValidation;
using LedMagazineBack.Models.ProductModels.CreationModels;

namespace LedMagazineBack.Validators.ProductValidations;

public class CreateProductValidator : AbstractValidator<CreateProductModel>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");
        When(x => x.Description is not null, () =>
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required")
                .MaximumLength(10000).WithMessage("Description must not exceed 10000 characters");
        });
        
        RuleFor(x=> x.BasePrice).NotEmpty().WithMessage("Price is required")
            .NotNull().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        
        RuleFor(x=> x.Duration)
            .NotEmpty().WithMessage("Duration is required")
            .NotNull().WithMessage("Duration is required")
            .MaximumLength(20).WithMessage("Duration must not exceed 20 characters");
        
        RuleFor(x=> x.Image)
            .NotNull().WithMessage("Image is required")
            .NotEmpty().WithMessage("Image cant be empty");
    }
}