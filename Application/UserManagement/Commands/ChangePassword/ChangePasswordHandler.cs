using Application.Shared.Exceptions;
using Application.Shared.Infrastructure;
using Domain.UserManagement.Repositories;
using MediatR;

namespace Application.UserManagement.Commands.ChangePassword;

public class ChangePasswordHandler(IUserRepository userRepository, ApplicationContext applicationContext)
    : IRequestHandler<ChangePassword>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ApplicationContext _applicationContext = applicationContext;

    public async Task<Unit> Handle(ChangePassword request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OfIdAsync(_applicationContext.UserId) 
            ?? throw new KeyNotFoundException("Invalid user Id");

        if (!await _userRepository.CheckPasswordAsync(user, request.CurrentPassword))
        {
            throw new KeyNotFoundException("Invalid user credentials.");
        }

        if (request.CurrentPassword == request.NewPassword)
        {
            throw new ApiException(System.Net.HttpStatusCode.BadRequest, "This password does not meet the length, complexity, age, or history requirements of password policy.");
        }

        var result = await _userRepository.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (!result.Succeeded)
        {
            throw new ApiException("User Password change failed");
        }

        return Unit.Value;
    }
}