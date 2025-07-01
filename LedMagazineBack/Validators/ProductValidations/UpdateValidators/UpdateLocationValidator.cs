using FluentValidation;
using LedMagazineBack.Models.ProductModels.UpdateModels;

namespace LedMagazineBack.Validators.ProductValidations.UpdateValidators;

public class UpdateLocationValidator : AbstractValidator<UpdateLocationModel>
{
    public UpdateLocationValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");

        When(x => x.District is not null, () =>
        {
            RuleFor(x => x.District)
                .NotEmpty().WithMessage("District is empty. Send null if dont want to update it");
        });
    }
}