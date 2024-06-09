using Application.Shared.Services.Abstract;
using Domain.Shared;
using Domain.UserManagement.Repositories;
using MediatR;
using System.IdentityModel.Tokens.Jwt;

namespace Application.IdentityManagement.Commands.CreateAuthenticationToken;

public class CreateAuthenticationTokenHandler(IUserRepository userRepository, ITokenService tokenService, IUnitOfWork unitOfWork) : IRequestHandler<CreateAuthenticationTokenRequest, CreateAuthenticationTokenResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreateAuthenticationTokenResponse> Handle(CreateAuthenticationTokenRequest request, CancellationToken cancellationToken)
    {
        var validateUser = await _userRepository.ValidateUserAsync(request.UserName, request.Password);

        if (validateUser.success && validateUser.user != null)
        {
            var user = validateUser.user;
            var roles = await _userRepository.GetUserRolesAsync(user);

            var signingCredentials = _tokenService.GetSigningCredentials();
            var claims = _tokenService.GetClaims(user, roles);
            var tokenOptions = _tokenService.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            await _unitOfWork.SaveAsync();

            return new CreateAuthenticationTokenResponse(true, token);
        }

        return new CreateAuthenticationTokenResponse(false, string.Empty);
    }
}