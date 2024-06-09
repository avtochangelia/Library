using FluentValidation;

namespace Application.AuthorManagement.Commands.CreateAuthor;

public class CreateAuthorValidator : AbstractValidator<CreateAuthor>
{
    public CreateAuthorValidator()
    {
        RuleFor(author => author.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

        RuleFor(author => author.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");

        RuleFor(author => author.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage("Date of birth must be in the past.");
    }
}