using Application.Shared.Exceptions;
using Application.Shared.Infrastructure;
using Domain.AuthorManagement.Repositories;
using Domain.BookManagement.Repositories;
using Domain.Shared;
using Domain.UserManagement.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Net;

namespace Application.BookManagement.Commands.UpdateBook;

public class UpdateBookHandler(
    IBookRepository bookRepository, 
    IAuthorRepository authorRepository, 
    IUnitOfWork unitOfWork, 
    ApplicationContext applicationContext) : IRequestHandler<UpdateBook>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ApplicationContext _applicationContext = applicationContext;

    public async Task<Unit> Handle(UpdateBook request, CancellationToken cancellationToken)
    {
        var book = _bookRepository.Query()
                                  .Where(x => x.Id == request.Id)
                                  .Include(x => x.Authors)
                                  .FirstOrDefault() ?? throw new KeyNotFoundException($"Book not found for Id: {request.Id}");

        if (_applicationContext.UserRole != UserRole.Admin && book.CreatorId != _applicationContext.UserId)
        {
            throw new ApiException(HttpStatusCode.Forbidden, "Permission denied");
        }

        book.ChangeDetails(request.Title, request.Description, request.Image, request.Rating);
        
        if (request.AuthorIds != null && request.AuthorIds.Any())
        {
            var existingAuthors = await _authorRepository.QueryAsync(x => request.AuthorIds.Distinct().Contains(x.Id));

            if (existingAuthors.Count != request.AuthorIds.Count())
            {
                throw new Exception("One or more author IDs provided are invalid.");
            }

            book.SetAuthors(existingAuthors);
        }

        _bookRepository.Update(book);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}