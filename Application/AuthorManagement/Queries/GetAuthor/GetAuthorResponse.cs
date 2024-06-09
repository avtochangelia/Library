using Application.AuthorManagement.Dtos;

namespace Application.AuthorManagement.Queries.GetAuthor;

public class GetAuthorResponse
{
    public AuthorDtoModel? Author { get; set; }
}