using FluentValidation;
using LedMagazineBack.Models.ProductModels.UpdateModels;
using LedMagazineBack.Models.UserModels.UpdateModels;

namespace LedMagazineBack.Validators.UserValidators;

public class ChangePhoneNumberValidation : AbstractValidator<UpdateContactNumberModel>
{
    public ChangePhoneNumberValidation()
    {
        RuleFor(x => x.ContactNumber).NotEmpty().WithMessage("Phone number cannot be empty")
            .NotNull().WithMessage("Phone number cannot be null")
            .Matches("^998\\d{9}$")
            .WithMessage("ContactNumber must not exceed 9 characters and should start from 998");
    }
}