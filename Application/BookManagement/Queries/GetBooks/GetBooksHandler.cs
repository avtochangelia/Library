using Application.BookManagement.Dtos;
using Domain.BookManagement;
using Domain.BookManagement.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

namespace Application.BookManagement.Queries.GetBooks;

public class GetBooksHandler(IBookRepository bookRepository) : IRequestHandler<GetBooksRequest, GetBooksResponse>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<GetBooksResponse> Handle(GetBooksRequest request, CancellationToken cancellationToken)
    {
        var title = nameof(Book.Title);

        var books = _bookRepository.Query()
                                   .Include(x => x.Authors)
                                   .ContainsIgnoreCase(request.SearchQuery, title)
                                   .And(request.Status, x => x.Status == request.Status)
                                   .OrderByDescending(x => x.CreateDateUtc);

        var total = books.Count();

        var booksList = await books.Pagination(request).ToListAsync(cancellationToken);

        var response = new GetBooksResponse
        {
            Books = booksList?.Select(x => BookDtoModel.MapToDto(x)),
            Page = request.Page,
            PageSize = request.PageSize,
            Total = total,
        };

        return response;
    }
}