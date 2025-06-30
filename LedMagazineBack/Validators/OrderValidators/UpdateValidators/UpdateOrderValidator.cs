using FluentValidation;
using LedMagazineBack.Models.OrderModels.UpdateModels;

namespace LedMagazineBack.Validators.OrderValidators.UpdateValidators;

public class UpdateOrderValidator : AbstractValidator<UpdateOrderModel>
{
    public UpdateOrderValidator()
    {
        When(x => x.OrganisationName is not null, () =>
        {
            RuleFor(x => x.OrganisationName).NotEmpty().WithMessage("Organisation name cannot be empty")
                .MaximumLength(30).WithMessage("Organisation name must be less than 30 characters");
        });

        When(x => x.PhoneNumber is not null, () =>
        {
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number cannot be empty")
                .Matches("^998\\d{9}$")
                .WithMessage("ContactNumber must not exceed 9 characters and should start from 998");
        });
    }
}