using Application.BookManagement.Dtos;
using Domain.AuthorManagement;

namespace Application.AuthorManagement.Dtos;

public class AuthorDtoModel : AuthorBaseDtoModel
{
    public IEnumerable<BookBaseDtoModel>? Books { get; set; }

    public static AuthorDtoModel MapToDto(Author author, bool includeNavProperties = true)
    {
        var model = new AuthorDtoModel()
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            DateOfBirth = author.DateOfBirth,
            CreatorId = author.CreatorId,
        };

        if (includeNavProperties)
        {
            model.Books = author.Books != null && author.Books.Count != 0 ? author.Books.Select(x => BookBaseDtoModel.MapToDto(x)).ToList() : default;
        }

        return model;
    }
}