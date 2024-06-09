using MediatR;

namespace Application.AuthorManagement.Commands.DeleteAuthor;

public class DeleteAuthor : IRequest
{
    public Guid AuthorId { get; set; }
}