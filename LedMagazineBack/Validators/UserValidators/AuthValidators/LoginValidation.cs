using FluentValidation;
using LedMagazineBack.Models.UserModels.Auth;

namespace LedMagazineBack.Validators.UserValidators.AuthValidators;

public class LoginValidation : AbstractValidator<LoginModel>
{
    public LoginValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .NotNull().WithMessage("Username is required")
            .Length(3, 30).WithMessage("Username must be between 3 and 30 characters");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .NotNull().WithMessage("Password is required")
            .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{8,}$")
            .WithMessage("Password should contain at least 8 characters and contain one capital letter, one special character and one digit at least");
    }
}