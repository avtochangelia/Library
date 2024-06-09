using FluentValidation;
using Library.App.Helpers;
using Library.App.Models.ViewModels.Identity;

namespace Library.App.Models.ViewModels.Identity.Validators;

public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
{
    public RegisterViewModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("სახელი არ უნდა იყოს ცარიელი.")
            .NotNull().WithMessage("სახელი არ უნდა იყოს ცარიელი.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("გვარი არ უნდა იყოს ცარიელი.")
            .NotNull().WithMessage("გვარი არ უნდა იყოს ცარიელი.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("ელ. ფოსტა არ უნდა იყოს ცარიელი.")
            .NotNull().WithMessage("ელ. ფოსტა არ უნდა იყოს ცარიელი.")
            .EmailAddress().WithMessage("ელ. ფოსტის მისამართი არასწორია.");

        RuleFor(x => x.UserName)
            .MinimumLength(5).WithMessage("მომხმარებლის სახელი უნდა იყოს მინიმუმ 5 სიმბოლო.")
            .MaximumLength(20).WithMessage("მომხმარებლის სახელი არ უნდა აღემატებოდეს 20 სიმბოლოს.")
            .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("მომხმარებლის სახელი უნდა შეიცავდეს მხოლოდ ასოებს, ციფრებს და ქვედა ტირეს.");

        ValidationHelper.AddPasswordRules(RuleFor(x => x.Password), "პაროლი");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("პაროლი არ ემთხვევა!");
    }
}