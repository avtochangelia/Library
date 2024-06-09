using Application.Shared.Configurations;
using Microsoft.Extensions.Configuration;

namespace DI;

public static class ConfigurationBinderExtensions
{
    public static void BindStringList<TOptions>(this IConfiguration configuration, string key, Action<TOptions, List<string>> propertySetter)
        where TOptions : class
    {
        var section = configuration.GetSection(key);

        if (section != null && section.Value != null)
        {
            var values = section.Get<string>()?.Split(',').ToList();
            var options = Activator.CreateInstance<TOptions>();

            propertySetter(options, values);
        }
    }

    public static void BindJwtConfigValidAudiences(this IConfiguration configuration, AppSettings appSettings)
    {
        configuration.BindStringList<JwtConfig>("jwtconfig:ValidAudiences", (options, values) =>
        {
            appSettings.JwtConfig.ValidAudiences = values;
        });
    }
}