#nullable disable

using Application.AuthorManagement.Dtos;
using Application.Shared;

namespace Application.AuthorManagement.Queries.GetAuthors;

public class GetAuthorsResponse : PaginationResponse
{
    public IEnumerable<AuthorDtoModel> Authors { get; set; }
}