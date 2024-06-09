using MediatR;

namespace Application.UserManagement.Queries;

public class GetUserRequest : IRequest<GetUserResponse>
{
    public Guid UserId { get; set; }
}