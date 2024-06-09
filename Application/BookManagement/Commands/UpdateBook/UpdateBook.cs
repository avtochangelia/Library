using MediatR;

namespace Application.BookManagement.Commands.UpdateBook;

public class UpdateBook : IRequest
{
    public Guid Id { get; private set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public double Rating { get; set; }
    public IEnumerable<Guid>? AuthorIds { get; set; }

    public void SetId(Guid id)
    {
        Id = id;
    }
}