using FluentValidation;
using Library.App.Helpers;

namespace Library.App.Models.ViewModels.Books.Validators;

public class UpdateBookModelValidator : AbstractValidator<UpdateBookModel>
{
    public UpdateBookModelValidator()
    {
        RuleFor(book => book.Title)
            .NotEmpty().WithMessage("სათაური სავალდებულოა.")
            .MaximumLength(100).WithMessage("სათაური არ უნდა აღემატებოდეს 100 სიმბოლოს.");

        RuleFor(book => book.Description)
            .NotEmpty().WithMessage("აღწერა სავალდებულოა.")
            .MaximumLength(500).WithMessage("აღწერა არ უნდა აღემატებოდეს 500 სიმბოლოს.");

        RuleFor(book => book.Image)
            .NotEmpty().WithMessage("სურათის მისამართი სავალდებულოა.")
            .Must(ValidationHelper.IsValidUrl).WithMessage("სურათის მისამართი უნდა იყოს სწორ ფორმატში.");

        RuleFor(book => book.Rating)
            .InclusiveBetween(0, 5).WithMessage("შეფასება უნდა იყოს 0-დან 5-ის ჩათვლით.");

        RuleFor(book => book.AuthorIds)
            .NotEmpty().WithMessage("სავალდებულოა მიუთითოთ მინიმუმ ერთი ავტორი.");
    }
}