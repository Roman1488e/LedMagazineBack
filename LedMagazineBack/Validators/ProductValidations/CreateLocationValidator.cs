using FluentValidation;
using LedMagazineBack.Models.ProductModels.CreationModels;

namespace LedMagazineBack.Validators.ProductValidations;

public class CreateLocationValidator : AbstractValidator<CreateLocationModel>
{
    public CreateLocationValidator()
    {
            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");

            RuleFor(x => x.District)
                .NotNull().WithMessage("District is required.")
                .NotEmpty().WithMessage("District is required.");

            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty).WithMessage("ProductId is empty.");
    }
}