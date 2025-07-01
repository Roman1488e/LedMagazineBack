using FluentValidation;
using LedMagazineBack.Models.CartModels.CreationModels;

namespace LedMagazineBack.Validators.CartValidators;

public class CreateCartItemValidator : AbstractValidator<CreateCartItemModel>
{
    public CreateCartItemValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId cannot be empty")
            .NotNull().WithMessage("ProductId cannot be null")
            .NotEqual(Guid.Empty).WithMessage("ProductId cannot be empty");
        RuleFor(X => X.RentMonths).NotNull().WithMessage("RentMonths cannot be empty")
            .Must(x=> x > 0).WithMessage("RentMonths must be greater than 0");
        RuleFor(x=> x.RentSeconds)
            .NotNull().WithMessage("RentSeconds cannot be empty")
            .Must(x=> x is 5 or 10 or 15).WithMessage("RentSeconds must be 5 or 10 or 15");
    }
}