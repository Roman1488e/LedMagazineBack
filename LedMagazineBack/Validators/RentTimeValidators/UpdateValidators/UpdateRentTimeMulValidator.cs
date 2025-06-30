using FluentValidation;
using LedMagazineBack.Models.RentTimeModels.UpdateModels;

namespace LedMagazineBack.Validators.RentTimeValidators.UpdateValidators;

public class UpdateRentTimeMulValidator : AbstractValidator<UpdateRentTimeMultModel>
{
    public UpdateRentTimeMulValidator()
    {
        RuleFor(x=> x.MonthsDifferenceMultiplayer)
            .NotNull().WithMessage("MonthsDifferenceMultiplayer cannot be empty")
            .InclusiveBetween(1,2).WithMessage("MonthsDifferenceMultiplayer must be between 1 and 2");
        RuleFor(x => x.SecondsDifferenceMultiplayer)
            .NotNull().WithMessage("SecondsDifferenceMultiplayer cannot be empty")
            .InclusiveBetween(1, 2).WithMessage("SecondsDifferenceMultiplayer must be between 1 and 2");
    }
}