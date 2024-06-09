using Application.Shared;
using MediatR;

namespace Application.AuthorManagement.Queries.GetAuthors;

public class GetAuthorsRequest : PaginationRequest, IRequest<GetAuthorsResponse>
{
    public string? SearchQuery { get; set; }
}