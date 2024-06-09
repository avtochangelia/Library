using Application.Shared.Exceptions;
using Application.Shared.Infrastructure;
using Domain.AuthorManagement.Repositories;
using Domain.Shared;
using Domain.UserManagement.Enums;
using MediatR;
using System.Net;

namespace Application.AuthorManagement.Commands.UpdateAuthor;

public class UpdateAuthorHandler(
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    ApplicationContext applicationContext) : IRequestHandler<UpdateAuthor>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ApplicationContext _applicationContext = applicationContext;

    public async Task<Unit> Handle(UpdateAuthor request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.OfIdAsync(request.Id)
            ?? throw new KeyNotFoundException($"Author not found for Id: {request.Id}");

        if (_applicationContext.UserRole != UserRole.Admin && author.CreatorId != _applicationContext.UserId)
        {
            throw new ApiException(HttpStatusCode.Forbidden, "Permission denied");
        }

        author.ChangeDetails(request.FirstName, request.LastName, request.DateOfBirth);

        _authorRepository.Update(author);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}