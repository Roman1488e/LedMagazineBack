using FluentValidation;
using LedMagazineBack.Models.RentTimeModels.CreationModels;

namespace LedMagazineBack.Validators.RentTimeValidators;

public class CreateRentTimeMulValidator : AbstractValidator<CreateRentTimeMulModel>
{
    public CreateRentTimeMulValidator()
    {
        RuleFor(x => x.MonthsDifferenceMultiplayer)
            .InclusiveBetween(1,2).WithMessage("Months difference must be between 1 and 2");
        RuleFor(x => x.SecondsDifferenceMultiplayer)
            .InclusiveBetween(1,2).WithMessage("Seconds difference must be between 1 and 2");
        RuleFor(x => x.ProductId)
            .NotNull().WithMessage("ProductId cannot be empty");
    }
}