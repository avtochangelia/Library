using Domain.UserManagement;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Shared.Services.Abstract;

public interface ITokenService
{
    SigningCredentials GetSigningCredentials();

    List<Claim> GetClaims(User user, IEnumerable<string> roles);

    JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string expiredToken);
}