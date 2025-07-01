using FluentValidation;
using LedMagazineBack.Models.ProductModels.UpdateModels;

namespace LedMagazineBack.Validators.ProductValidations.UpdateValidators;

public class UpdatePriceValidator :  AbstractValidator<UpdateProductPriceModel>
{
    public UpdatePriceValidator()
    {
        RuleFor(x=>x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0");
    }
}