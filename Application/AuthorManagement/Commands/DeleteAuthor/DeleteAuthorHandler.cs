using Application.Shared.Exceptions;
using Application.Shared.Infrastructure;
using Domain.AuthorManagement.Repositories;
using Domain.Shared;
using Domain.UserManagement.Enums;
using MediatR;
using System.Net;

namespace Application.AuthorManagement.Commands.DeleteAuthor;

public class DeleteAuthorHandler(IUnitOfWork unitOfWork, IAuthorRepository authorRepository, ApplicationContext applicationContext) : IRequestHandler<DeleteAuthor>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ApplicationContext _applicationContext = applicationContext;

    public async Task<Unit> Handle(DeleteAuthor request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.OfIdAsync(request.AuthorId) ?? throw new KeyNotFoundException($"Author not found for Id: {request.AuthorId}");
        
        if (_applicationContext.UserRole != UserRole.Admin && author.CreatorId != _applicationContext.UserId)
        {
            throw new ApiException(HttpStatusCode.Forbidden, "Permission denied");
        }

        _authorRepository.Delete(author);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}