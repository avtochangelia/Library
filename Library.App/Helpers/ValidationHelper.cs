using FluentValidation;

namespace Library.App.Helpers;

public static class ValidationHelper
{
    public static void AddPasswordRules<T>(IRuleBuilder<T, string> ruleBuilder, string propertyName)
    {
        ruleBuilder
            .NotEmpty().WithMessage($"{propertyName} არ უნდა იყოს ცარიელი.")
            .NotNull().WithMessage($"{propertyName} არ უნდა იყოს ცარიელი.")
            .MinimumLength(8).WithMessage($"{propertyName} უნდა იყოს მინიმუმ 8 სიმბოლოიანი.")
            .Matches(@"[A-Z]").WithMessage($"{propertyName} უნდა შეიცავდეს მინიმუმ ერთ დიდ ასოს.")
            .Matches(@"[a-z]").WithMessage($"{propertyName} უნდა შეიცავდეს მინიმუმ ერთ პატარა ასოს.")
            .Matches(@"\d").WithMessage($"{propertyName} უნდა შეიცავდეს მინიმუმ ერთ ციფრს.")
            .Matches(@"[\W_]").WithMessage($"{propertyName} უნდა შეიცავდეს მინიმუმ ერთ სპეციალურ სიმბოლოს.");
    }

    public static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}