using FluentValidation;
using LedMagazineBack.Models.RentTimeModels.UpdateModels;

namespace LedMagazineBack.Validators.RentTimeValidators.UpdateValidators;

public class UpdateRentTimeValidator : AbstractValidator<UpdateRentTimeModel>
{
    public UpdateRentTimeValidator()
    {
        RuleFor(x=> x.RentMonths)
            .NotNull().WithMessage("RentMonths cannot be empty")
            .Must(x=> x >0).WithMessage("RentMonths must be greater than 0");
        RuleFor(x=> x.RentSeconds)
            .NotNull().WithMessage("RentSeconds cannot be empty")
            .Must(x=> x is 5 or 10 or 15).WithMessage("RentSeconds must be 5 or 10 or 15");;
    }
}