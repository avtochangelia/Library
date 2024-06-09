using Domain.BookManagement.Enums;
using Domain.BookManagement.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.BookManagement.Commands.TakeOutBook;

public class TakeOutBookHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository) : IRequestHandler<TakeOutBook>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Unit> Handle(TakeOutBook request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.OfIdAsync(request.BookId) ?? throw new KeyNotFoundException($"Book not found for Id: {request.BookId}");
        
        if (book.Status == BookStatus.Taken)
        {
            throw new InvalidOperationException("The book is already taken out.");
        }

        book.MarkAsTaken();
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}