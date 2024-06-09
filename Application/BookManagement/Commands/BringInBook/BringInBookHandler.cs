using Domain.BookManagement.Enums;
using Domain.BookManagement.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.BookManagement.Commands.BringInBook;

public class BringInBookHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository) : IRequestHandler<BringInBook>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Unit> Handle(BringInBook request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.OfIdAsync(request.BookId);

        if (book == null)
        {
            throw new KeyNotFoundException($"Book not found for Id: {request.BookId}");
        }

        if (book.Status == BookStatus.Available)
        {
            throw new InvalidOperationException("The book is already available.");
        }

        book.MarkAsAvailable();
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}