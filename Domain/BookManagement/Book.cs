using Domain.AuthorManagement;
using Domain.BookManagement.Enums;
using Domain.Shared;

namespace Domain.BookManagement;

public class Book : BaseEntity<Guid>
{
    public Book()
    {
        Title = string.Empty;
        Description = string.Empty;
        Image = string.Empty;
        CreatorId = string.Empty;
    }

    public Book(string title, string description, string image, double rating, string creatorId)
    {
        Title = title;
        Description = description;
        Image = image;
        Rating = rating;
        Status = BookStatus.Available;
        CreatorId = creatorId;
        CreateDateUtc = DateTimeOffset.UtcNow;
    }

    public override Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public double Rating { get; set; }
    public BookStatus Status { get; set; }
    public DateTimeOffset CreateDateUtc { get; private set; }
    public DateTimeOffset? UpdateDateUtc { get; private set; }
    public string CreatorId { get; set; }

    public virtual ICollection<Author>? Authors { get; set; }

    public void ChangeDetails(string title, string description, string image, double rating)
    {
        Title = title;
        Description = description;
        Image = image;
        Rating = rating;
        UpdateDateUtc = DateTimeOffset.UtcNow;
    }

    public void MarkAsAvailable()
    {
        Status = BookStatus.Available;
    }

    public void MarkAsTaken()
    {
        Status = BookStatus.Taken;
    }

    public void SetAuthors(IEnumerable<Author> authors)
    {
        Authors ??= new List<Author>();

        Authors.Clear();

        foreach (var author in authors)
        {
            Authors.Add(author);
        }
    }
}