using FluentValidation;

namespace Application.UserManagement.Commands.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUser>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName must not be empty.")
            .NotNull().WithMessage("FirstName must not be null.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName must not be empty.")
            .NotNull().WithMessage("LastName must not be null.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must not be empty.")
            .NotNull().WithMessage("Email must not be null.")
            .EmailAddress().WithMessage("The email address provided is not valid.");

        RuleFor(x => x.UserName)
            .MinimumLength(5)
            .MaximumLength(20)
            .Matches(@"^[a-zA-Z0-9_]+$")
            .WithMessage("Username can only contain letters, numbers, and underscores.");
    }
}