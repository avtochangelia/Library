using FluentValidation;
using Library.App.Helpers;

namespace Library.App.Models.ViewModels.Account.Validators;

public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
{
    public ChangePasswordViewModelValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("ახალი პაროლი არ უნდა იყოს ცარიელი.")
            .NotNull().WithMessage("ახალი პაროლი არ უნდა იყოს ცარიელი.");

        RuleFor(x => x.ConfirmNewPassword)
            .Equal(x => x.NewPassword).WithMessage("პაროლი არ ემთხვევა!");

        ValidationHelper.AddPasswordRules(RuleFor(x => x.NewPassword), "ახალი");
    }
}