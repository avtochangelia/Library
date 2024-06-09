using Domain.BookManagement;
using Domain.Shared;

namespace Domain.AuthorManagement;

public class Author : BaseEntity<Guid>
{
    public Author()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        CreatorId = string.Empty;
    }

    public Author(string firstName, string lastName, DateTime dateOfBirth, string creatorId)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = DateTime.SpecifyKind(dateOfBirth, DateTimeKind.Utc);
        CreatorId = creatorId;
        CreateDateUtc = DateTimeOffset.UtcNow;
    }

    public override Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTimeOffset CreateDateUtc { get; private set; }
    public DateTimeOffset? UpdateDateUtc { get; private set; }
    public string CreatorId { get; set; }

    public virtual ICollection<Book>? Books { get; set; }

    public void ChangeDetails(string firstName, string lastName, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = DateTime.SpecifyKind(dateOfBirth, DateTimeKind.Utc);
        UpdateDateUtc = DateTimeOffset.UtcNow;
    }
}