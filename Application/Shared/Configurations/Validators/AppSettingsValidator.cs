using FluentValidation;

namespace Application.Shared.Configurations.Validators;

public class AppSettingsValidator : AbstractValidator<AppSettings>
{
    public AppSettingsValidator()
    {
        RuleFor(v => v.ConnectionStrings).NotNull()
            .SetValidator(new ConnectionStringsConfigValidator());

        RuleFor(v => v.JwtConfig).NotNull()
            .SetValidator(new JWTConfigValidator());

        RuleFor(v => v.ConnectionStrings).NotNull()
            .SetValidator(new ConnectionStringsConfigValidator());
    }

    internal class ConnectionStringsConfigValidator : AbstractValidator<ConnectionStringsConfig>
    {
        public ConnectionStringsConfigValidator()
        {
            RuleFor(x => x.Default).NotNull().NotEmpty();
        }
    }

    internal class JWTConfigValidator : AbstractValidator<JwtConfig>
    {
        public JWTConfigValidator()
        {
            RuleFor(x => x.ValidIssuer).NotNull().NotEmpty();
            RuleFor(x => x.ValidAudience).NotNull().NotEmpty();
            RuleFor(x => x.SecretKey).NotNull().NotEmpty();
            RuleFor(x => x.ExpiresInMinutes).NotNull().NotEmpty();
        }
    }
}