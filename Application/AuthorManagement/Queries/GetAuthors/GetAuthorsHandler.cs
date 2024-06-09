using Application.AuthorManagement.Dtos;
using Domain.AuthorManagement;
using Domain.AuthorManagement.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

namespace Application.AuthorManagement.Queries.GetAuthors;

public class GetAuthorsHandler(IAuthorRepository authorRepository) : IRequestHandler<GetAuthorsRequest, GetAuthorsResponse>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<GetAuthorsResponse> Handle(GetAuthorsRequest request, CancellationToken cancellationToken)
    {
        var firstName = nameof(Author.FirstName);
        var lastName = nameof(Author.LastName);

        var authors = _authorRepository.Query()
                                       .Include(x => x.Books)
                                       .ContainsIgnoreCase(request.SearchQuery, firstName, lastName)
                                       .OrderByDescending(x => x.CreateDateUtc);

        var total = authors.Count();

        var authorsList = await authors.Pagination(request).ToListAsync(cancellationToken);

        var response = new GetAuthorsResponse
        {
            Authors = authorsList?.Select(x => AuthorDtoModel.MapToDto(x)),
            Page = request.Page,
            PageSize = request.PageSize,
            Total = total,
        };

        return response;
    }
}