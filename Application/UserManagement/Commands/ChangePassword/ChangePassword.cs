using MediatR;

namespace Application.UserManagement.Commands.ChangePassword;

public class ChangePassword : IRequest
{
    public ChangePassword()
    {
        NewPassword = string.Empty;
        CurrentPassword = string.Empty;
    }

    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}