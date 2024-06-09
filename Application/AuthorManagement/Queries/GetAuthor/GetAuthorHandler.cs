using Application.AuthorManagement.Dtos;
using Domain.AuthorManagement.Repositories;
using MediatR;

namespace Application.AuthorManagement.Queries.GetAuthor;

public class GetAuthorHandler(IAuthorRepository authorRepository) : IRequestHandler<GetAuthorRequest, GetAuthorResponse>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<GetAuthorResponse> Handle(GetAuthorRequest request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.OfIdAsync(request.AuthorId) 
            ?? throw new KeyNotFoundException($"Author was not found for Id: {request.AuthorId}");
        
        return new GetAuthorResponse()
        {
            Author = AuthorDtoModel.MapToDto(author),
        };
    }
}