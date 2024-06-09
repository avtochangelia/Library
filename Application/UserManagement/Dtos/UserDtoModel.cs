#nullable disable

using Domain.UserManagement.Enums;
using Domain.UserManagement;

namespace Application.UserManagement.Dtos;

public class UserDtoModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserStatus Status { get; set; }

    public static UserDtoModel MapToDto(User user)
    {
        var model = new UserDtoModel
        {
            Id = Guid.Parse(user.Id),
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            Status = user.Status
        };

        return model;
    }
}