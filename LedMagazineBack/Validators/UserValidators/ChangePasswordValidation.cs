using FluentValidation;
using LedMagazineBack.Models.UserModels.UpdateModels;

namespace LedMagazineBack.Validators.UserValidators;

public class ChangePasswordValidation : AbstractValidator<UpdatePasswordModel>
{
    public ChangePasswordValidation()
    {
        RuleFor(x=> x.NewPassword)
            .NotNull().WithMessage("Password is required")
            .NotEmpty().WithMessage("New password is required")
            .Equal(x => x.ConfirmNewPassword)
            .WithMessage("Password and confirm password do not match")
            .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{8,}$")
            .WithMessage("Password should contain at least 8 characters and contain one capital letter, one special character and one digit at least");
        
        RuleFor(x => x.OldPassword)
            .NotNull().WithMessage("Old password is required")
            .NotEmpty().WithMessage("Old password is required")
            .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{8,}$")
            .WithMessage("Password should contain at least 8 characters and contain one capital letter, one special character and one digit at least");
    }
}