using Domain.Shared;
using Domain.UserManagement.Repositories;
using MediatR;

namespace Application.UserManagement.Commands.UpdateUser;

public class UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUser>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OfIdAsync(request.Id.ToString());

        if (user == null)
        {
            throw new KeyNotFoundException($"User was not found for Id: {request.Id}");
        }

        if (user.Email != request.Email)
        {
            var existingUserWithEmail = await _userRepository.FindByEmailAsync(request.Email);

            if (existingUserWithEmail != null && existingUserWithEmail.Id != user.Id)
            {
                throw new InvalidOperationException("Email is already in use.");
            }
        }

        if (user.UserName != request.UserName)
        {
            var existingUserWithUsername = await _userRepository.FindByNameAsync(request.UserName);

            if (existingUserWithUsername != null && existingUserWithUsername.Id != user.Id)
            {
                throw new InvalidOperationException("Username is already taken.");
            }
        }

        user.ChangeDetails(request.FirstName, request.LastName);

        if (user.UserName != request.UserName)
        {
            _ = await _userRepository.ChangeUserNameAsync(user, request.UserName);
        }

        if (user.Email != request.Email)
        {
            _ = await _userRepository.ChangeEmailAsync(user, request.Email);
        }

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}