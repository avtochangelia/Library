using MediatR;

namespace Application.UserManagement.Commands.UpdateUser;

public record UpdateUser(Guid Id, string FirstName, string LastName, string UserName, string Email) : IRequest;