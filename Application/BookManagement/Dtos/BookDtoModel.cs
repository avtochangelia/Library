using Application.AuthorManagement.Dtos;
using Domain.BookManagement;

namespace Application.BookManagement.Dtos;

public class BookDtoModel : BookBaseDtoModel
{
    public IEnumerable<AuthorBaseDtoModel>? Authors { get; set; }

    public static BookDtoModel MapToDto(Book book, bool includeNavProperties = true)
    {
        var model = new BookDtoModel()
        {
            Id = book.Id,
            Title = book.Title,
            Description = book.Description,
            Image = book.Image,
            Rating = book.Rating,
            Status = book.Status,
            CreatorId = book.CreatorId,
        };

        if (includeNavProperties)
        {
            model.Authors = book.Authors != null && book.Authors.Count != 0 ? book.Authors.Select(x => new AuthorBaseDtoModel { Id = x.Id, FirstName = x.FirstName, LastName = x.LastName, DateOfBirth = x.DateOfBirth }).ToList() : default;
        }

        return model;
    }
}