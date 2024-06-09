using Application.Shared.Infrastructure;
using Domain.AuthorManagement.Repositories;
using Domain.BookManagement;
using Domain.BookManagement.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.BookManagement.Commands.CreateBook;

public class CreateBookHandler(
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork, 
    ApplicationContext applicationContext) : IRequestHandler<CreateBook>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ApplicationContext _applicationContext = applicationContext;

    public async Task<Unit> Handle(CreateBook request, CancellationToken cancellationToken)
    {
        var book = new Book(request.Title, request.Description, request.Image, request.Rating, _applicationContext.UserId);

        if (request.AuthorIds != null && request.AuthorIds.Any())
        {
            var existingAuthors = await _authorRepository.QueryAsync(x => request.AuthorIds.Distinct().Contains(x.Id));

            if (existingAuthors.Count != request.AuthorIds.Count())
            {
                throw new Exception("One or more author IDs provided are invalid.");
            }

            book.SetAuthors(existingAuthors);
        }

        await _bookRepository.InsertAsync(book);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}