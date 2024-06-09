using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Library.App.Middlewares;

public class JwtMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var token = context.Session.GetString("Token");
        if (!string.IsNullOrEmpty(token))
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is JwtSecurityToken jwtToken)
            {
                var claims = jwtToken.Claims;

                var identity = new ClaimsIdentity(claims, "Jwt");
                var principal = new ClaimsPrincipal(identity);
                context.User = principal;
            }
        }

        await _next(context);
    }
}