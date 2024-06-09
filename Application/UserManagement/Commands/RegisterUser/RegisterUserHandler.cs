using Domain.UserManagement.Enums;
using Domain.UserManagement.Repositories;
using Domain.UserManagement;
using MediatR;

namespace Application.UserManagement.Commands.RegisterUser;

public class RegisterUserHandler(IUserRepository userRepository)
    : IRequestHandler<RegisterUser>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Unit> Handle(RegisterUser request, CancellationToken cancellationToken)
    {
        var existingUserByEmail = await _userRepository.FindByEmailAsync(request.Email);

        if (existingUserByEmail != null)
        {
            throw new InvalidOperationException("Email already in use.");
        }

        var existingUserByName = await _userRepository.FindByNameAsync(request.UserName);

        if (existingUserByName != null)
        {
            throw new InvalidOperationException("Username is already taken.");
        }

        var user = new User(request.FirstName, request.LastName, request.UserName, request.Email);

        _ = await _userRepository.InsertAsync(user, request.Password, UserRole.User);

        return Unit.Value;
    }
}