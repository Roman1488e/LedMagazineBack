using FluentValidation;
using LedMagazineBack.Constants;
using LedMagazineBack.Models.UserModels.UpdateModels;

namespace LedMagazineBack.Validators.UserValidators;

public class ChangeRoleValidation : AbstractValidator<UpdateRoleModel>
{
    private readonly RolesConstants  _rolesConstants = new RolesConstants();
    public ChangeRoleValidation()
    {
        When(x => x.Role is not null, () =>
        {
            RuleFor(x => x.Role)
                .NotNull().WithMessage("Role cannot be null")
                .NotEmpty().WithMessage("Role cannot be empty")
                .Must(x => x.ToLower() == _rolesConstants.Admin || x.ToLower() == _rolesConstants.Guest ||
                           x.ToLower() == _rolesConstants.Customer)
                .WithMessage("Role must be either Admin ,Guest  or Customer");
        });
    }
}