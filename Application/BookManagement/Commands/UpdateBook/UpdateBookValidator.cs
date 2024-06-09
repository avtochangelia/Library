using Application.Shared.Helpers;
using FluentValidation;

namespace Application.BookManagement.Commands.UpdateBook;

public class UpdateBookValidator : AbstractValidator<UpdateBook>
{
    public UpdateBookValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(book => book.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(book => book.Image)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(ValidationHelper.IsValidUrl).WithMessage("Image URL must be a valid URL.");

        RuleFor(book => book.Rating)
            .InclusiveBetween(0, 5).WithMessage("Rating must be between 0 and 5.");
    }
}