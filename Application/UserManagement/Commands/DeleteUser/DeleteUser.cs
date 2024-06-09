using MediatR;

namespace Application.UserManagement.Commands.DeleteUser;

public class DeleteUser : IRequest
{
    public Guid UserId { get; set; }
}