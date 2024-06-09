using Application.Shared.Configurations;
using Application.Shared.Helpers;
using Application.Shared.Services.Abstract;
using Domain.UserManagement;
using Microsoft.IdentityModel.Tokens;
using Shared.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Shared.Services.Concrete;

public class TokenService(AppSettings appSettings) : ITokenService
{
    private readonly AppSettings _appSettings = appSettings;

    public SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = _appSettings.JwtConfig;
        var key = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public List<Claim> GetClaims(User user, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
        {
            new(UserClaims.Username, user.UserName!),
            new(UserClaims.UserId, user.Id),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(UserClaims.Role, role));
        }

        return claims;
    }

    public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _appSettings.JwtConfig;

        var tokenOptions = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings.ExpiresInMinutes)),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string expiredToken)
    {
        var tokenValidationParameters = TokenHelper.GetTokenValidationParametersForExpiredToken(_appSettings);

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(expiredToken, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }
}