#nullable disable

using Domain.BookManagement;
using Domain.BookManagement.Enums;

namespace Application.BookManagement.Dtos;

public class BookBaseDtoModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public double Rating { get; set; }
    public BookStatus Status { get; set; }
    public string CreatorId { get; set; }

    public static BookBaseDtoModel MapToDto(Book book)
    {
        return BookDtoModel.MapToDto(book, false);
    }
}