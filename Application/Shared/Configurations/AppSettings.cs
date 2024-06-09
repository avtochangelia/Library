#nullable disable

namespace Application.Shared.Configurations;

public class AppSettings
{
    public ConnectionStringsConfig ConnectionStrings { get; set; }
    public JwtConfig JwtConfig { get; set; }
}