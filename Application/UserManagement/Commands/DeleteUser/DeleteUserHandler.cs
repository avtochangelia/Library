using Application.Shared.Exceptions;
using Application.Shared.Infrastructure;
using Domain.UserManagement.Enums;
using Domain.UserManagement.Repositories;
using MediatR;
using System.Net;

namespace Application.UserManagement.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUserRepository userRepository, ApplicationContext applicationContext)
    : IRequestHandler<DeleteUser>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ApplicationContext _applicationContext = applicationContext;

    public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OfIdAsync(request.UserId.ToString());

        if (user == null)
        {
            throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
        }

        if (_applicationContext.UserRole != UserRole.Admin && _applicationContext.UserId != user.Id)
        {
            throw new ApiException(HttpStatusCode.Forbidden, "Permission denied");
        }

        _ = await _userRepository.DeleteAsync(user);

        return Unit.Value;
    }
}