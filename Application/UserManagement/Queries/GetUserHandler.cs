using Application.Shared.Exceptions;
using Application.UserManagement.Dtos;
using Domain.UserManagement.Enums;
using Domain.UserManagement.Repositories;
using MediatR;
using System.Net;

namespace Application.UserManagement.Queries;

public class GetUserHandler(IUserRepository userRepository) : IRequestHandler<GetUserRequest, GetUserResponse>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.OfIdAsync(request.UserId.ToString()) 
            ?? throw new KeyNotFoundException($"Uwer was not found for Id: {request.UserId}");

        if (user.Status == UserStatus.Inactive)
        {
            throw new ApiException(HttpStatusCode.Forbidden, $"User with Id: {request.UserId} is inactive.");
        }

        return new GetUserResponse()
        {
            User = UserDtoModel.MapToDto(user),
        };
    }
}