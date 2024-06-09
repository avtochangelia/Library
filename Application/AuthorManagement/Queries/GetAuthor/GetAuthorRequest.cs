using MediatR;

namespace Application.AuthorManagement.Queries.GetAuthor;

public class GetAuthorRequest : IRequest<GetAuthorResponse>
{
    public Guid AuthorId { get; set; }
}