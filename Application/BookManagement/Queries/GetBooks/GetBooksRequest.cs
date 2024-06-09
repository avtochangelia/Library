using Application.Shared;
using Domain.BookManagement.Enums;
using MediatR;

namespace Application.BookManagement.Queries.GetBooks;

public class GetBooksRequest : PaginationRequest, IRequest<GetBooksResponse>
{
    public string? SearchQuery { get; set; }

    public BookStatus? Status { get; set; }
}