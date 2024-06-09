#nullable disable

using Domain.AuthorManagement;

namespace Application.AuthorManagement.Dtos;

public class AuthorBaseDtoModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CreatorId { get; set; }

    public static AuthorBaseDtoModel MapToDto(Author author)
    {
        return AuthorDtoModel.MapToDto(author, false);
    }
}