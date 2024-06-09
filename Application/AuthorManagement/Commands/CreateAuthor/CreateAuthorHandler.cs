using Application.Shared.Infrastructure;
using Domain.AuthorManagement;
using Domain.AuthorManagement.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.AuthorManagement.Commands.CreateAuthor;

public class CreateAuthorHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ApplicationContext applicationContext) : IRequestHandler<CreateAuthor>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ApplicationContext _applicationContext = applicationContext;

    public async Task<Unit> Handle(CreateAuthor request, CancellationToken cancellationToken)
    {
        var author = new Author(request.FirstName, request.LastName, request.DateOfBirth, _applicationContext.UserId);

        await _authorRepository.InsertAsync(author);
        await _unitOfWork.SaveAsync();
        
        return Unit.Value;
    }
}