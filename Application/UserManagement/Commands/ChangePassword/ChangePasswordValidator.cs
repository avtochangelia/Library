using Application.Shared.Helpers;
using FluentValidation;

namespace Application.UserManagement.Commands.ChangePassword;

public class ChangePasswordValidator : AbstractValidator<ChangePassword>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Current password must not be empty.")
            .NotNull().WithMessage("Current password must not be null.");

        ValidationHelper.AddPasswordRules(RuleFor(x => x.NewPassword), "New password");
    }
}