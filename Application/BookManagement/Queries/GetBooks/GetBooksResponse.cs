#nullable disable

using Application.BookManagement.Dtos;
using Application.Shared;

namespace Application.BookManagement.Queries.GetBooks;

public class GetBooksResponse : PaginationResponse
{
    public IEnumerable<BookDtoModel> Books { get; set; }
}