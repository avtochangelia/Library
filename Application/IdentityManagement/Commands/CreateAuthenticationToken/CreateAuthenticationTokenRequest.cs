using MediatR;

namespace Application.IdentityManagement.Commands.CreateAuthenticationToken;

public record class CreateAuthenticationTokenRequest(string UserName, string Password) : IRequest<CreateAuthenticationTokenResponse>;