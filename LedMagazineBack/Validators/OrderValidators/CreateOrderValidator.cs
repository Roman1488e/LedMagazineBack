using FluentValidation;
using LedMagazineBack.Models.OrderModels.CreationModels;

namespace LedMagazineBack.Validators.OrderValidators;

public class CreateOrderValidator : AbstractValidator<CreateOrderModel>
{
    public CreateOrderValidator()
    {
        RuleFor(x=> x.OrganisationName).NotNull().WithMessage("OrganisationName cannot be null")
            .NotEmpty().WithMessage("OrganisationName cannot be empty")
            .MaximumLength(30).WithMessage("OrganisationName must be less than 30 characters");
        RuleFor(x=> x.PhoneNumber).NotNull().WithMessage("PhoneNumber cannot be null")
            .NotEmpty().WithMessage("PhoneNumber cannot be empty")
            .Matches("^998\\d{9}$")
            .WithMessage("ContactNumber must not exceed 9 characters and should start from 998");
    }
}