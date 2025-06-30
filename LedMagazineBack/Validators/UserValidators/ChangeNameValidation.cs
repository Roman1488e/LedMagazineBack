using FluentValidation;
using LedMagazineBack.Models.UserModels.UpdateModels;

namespace LedMagazineBack.Validators.UserValidators;

public class ChangeNameValidation : AbstractValidator<UpdateClientGenInfModel>
{
    public ChangeNameValidation()
    {
        RuleFor(x => x.Name).NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters");
    }
    
}