using FluentValidation;

namespace Application.Shared.Helpers;

public static class ValidationHelper
{
    public static void AddPasswordRules<T>(IRuleBuilder<T, string> ruleBuilder, string propertyName)
    {
        ruleBuilder
            .NotEmpty().WithMessage($"{propertyName} must not be empty.")
            .NotNull().WithMessage($"{propertyName} must not be null.")
            .MinimumLength(8).WithMessage($"{propertyName} must be at least 8 characters long.")
            .Matches(@"[A-Z]").WithMessage($"{propertyName} must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage($"{propertyName} must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage($"{propertyName} must contain at least one digit.")
            .Matches(@"[\W_]").WithMessage($"{propertyName} must contain at least one special character.");
    }

    public static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}