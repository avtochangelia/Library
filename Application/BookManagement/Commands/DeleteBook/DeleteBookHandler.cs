using Application.Shared.Exceptions;
using Application.Shared.Infrastructure;
using Domain.BookManagement.Repositories;
using Domain.Shared;
using Domain.UserManagement.Enums;
using Domain.UserManagement;
using MediatR;
using System.Net;

namespace Application.BookManagement.Commands.DeleteBook;

public class DeleteBookHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository, ApplicationContext applicationContext) : IRequestHandler<DeleteBook>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ApplicationContext _applicationContext = applicationContext;

    public async Task<Unit> Handle(DeleteBook request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.OfIdAsync(request.BookId);

        if (book == null)
        {
            throw new KeyNotFoundException($"Book not found for Id: {request.BookId}");
        }

        if (_applicationContext.UserRole != UserRole.Admin && book.CreatorId != _applicationContext.UserId)
        {
            throw new ApiException(HttpStatusCode.Forbidden, "Permission denied");
        }

        _bookRepository.Delete(book);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}