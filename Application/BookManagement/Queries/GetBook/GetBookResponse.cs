using Application.BookManagement.Dtos;

namespace Application.BookManagement.Queries.GetBook;

public class GetBookResponse
{
    public BookDtoModel? Book { get; set; }
}