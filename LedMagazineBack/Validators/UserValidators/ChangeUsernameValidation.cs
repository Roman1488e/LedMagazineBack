using FluentValidation;
using LedMagazineBack.Models.UserModels.UpdateModels;

namespace LedMagazineBack.Validators.UserValidators;

public class ChangeUsernameValidation : AbstractValidator<UpdateUsernameModel>
{
    public ChangeUsernameValidation()
    {
        When(x => x.Username != null, () =>
        {
            RuleFor(model => model.Username)
                .NotEmpty().WithMessage("Username cannot be empty")
                .NotNull().WithMessage("Username cannot be null")
                .Length(3, 30).WithMessage("Username must be between 3 and 30 characters");
        });

    }
}