using FluentValidation;

namespace Library.App.Models.ViewModels.Authors.Validators;

public class CreateAuthorModelValidator : AbstractValidator<CreateAuthorModel>
{
    public CreateAuthorModelValidator()
    {
        RuleFor(author => author.FirstName)
            .NotEmpty().WithMessage("სახელი სავალდებულოა.")
            .MaximumLength(50).WithMessage("სახელი არ უნდა აღემატებოდეს 50 სიმბოლოს.");

        RuleFor(author => author.LastName)
            .NotEmpty().WithMessage("გვარი სავალდებულოა.")
            .MaximumLength(50).WithMessage("გვარი არ უნდა აღემატებოდეს 50 სიმბოლოს.");

        RuleFor(author => author.DateOfBirth)
            .LessThan(DateTime.Now).WithMessage("დაბადების თარიღი არასწორია.");
    }
}