using Application.UserManagement.Dtos;

namespace Application.UserManagement.Queries;

public class GetUserResponse
{
    public UserDtoModel? User { get; set; }
}