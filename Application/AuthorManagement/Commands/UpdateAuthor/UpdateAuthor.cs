using MediatR;

namespace Application.AuthorManagement.Commands.UpdateAuthor;

public class UpdateAuthor : IRequest
{
    public Guid Id { get; private set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    public void SetId(Guid id)
    {
        Id = id;
    }
}