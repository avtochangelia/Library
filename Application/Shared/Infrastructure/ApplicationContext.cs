#nullable disable

using Domain.UserManagement.Enums;

namespace Application.Shared.Infrastructure;

public class ApplicationContext
{
    public string UserId { get; set; }
    public string Username { get; set; }
    public UserRole UserRole { get; set; }
}