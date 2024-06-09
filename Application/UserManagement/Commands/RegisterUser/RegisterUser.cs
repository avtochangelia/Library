using MediatR;

namespace Application.UserManagement.Commands.RegisterUser;

public record RegisterUser(string FirstName, string LastName, string UserName, string Email, string Password) : IRequest;