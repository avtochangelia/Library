using Application.BookManagement.Dtos;
using Domain.BookManagement.Repositories;
using MediatR;

namespace Application.BookManagement.Queries.GetBook;

public class GetBookHandler(IBookRepository bookRepository) : IRequestHandler<GetBookRequest, GetBookResponse>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<GetBookResponse> Handle(GetBookRequest request, CancellationToken cancellationToken)
    {
        var Book = await _bookRepository.OfIdAsync(request.BookId) 
            ?? throw new KeyNotFoundException($"Book was not found for Id: {request.BookId}");

        return new GetBookResponse()
        {
            Book = BookDtoModel.MapToDto(Book),
        };
    }
}