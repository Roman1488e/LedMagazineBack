using FluentValidation;
using LedMagazineBack.Models.UserModels.Auth;

namespace LedMagazineBack.Validators.UserValidators.AuthValidators;

public class RegisterValidation : AbstractValidator<RegisterModel>
{
    public RegisterValidation()
    {
        RuleFor(x=> x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .NotNull()
            .WithMessage("Name is required")
            .MaximumLength(50)
            .WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.ContactNumber)
            .NotEmpty()
            .WithMessage("ContactNumber is required")
            .NotNull()
            .WithMessage("ContactNumber is required")
            .Matches("^998\\d{9}$")
            .WithMessage("ContactNumber must not exceed 9 characters and should start from 998");
        
        RuleFor(x=> x.Username)
            .NotEmpty()
            .WithMessage("Username is required")
            .NotNull()
            .WithMessage("Username is required")
            .Length(3, 30)
            .WithMessage("Username should be between 3 and 30 characters");
        
        RuleFor(x=> x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .NotNull()
            .WithMessage("Password is required")
            .Equal(x => x.ConfirmPassword)
            .WithMessage("Password and confirm password do not match")
            .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[\\W_]).{8,}$")
            .WithMessage("Password should contain at least 8 characters and contain one capital letter, one special character and one digit at least");
        
        RuleFor(x=> x.OrganisationName)
            .NotEmpty()
            .WithMessage("OrganisationName is required")
            .NotNull()
            .WithMessage("OrganisationName is required")
            .MaximumLength(50)
            .WithMessage("OrganisationName must not exceed 50 characters");
    }
    
}