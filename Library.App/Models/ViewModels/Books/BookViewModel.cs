#nullable disable

using Library.App.Models.ViewModels.Authors;
using Library.App.Models.ViewModels.Books.Enums;

namespace Library.App.Models.ViewModels.Books;

public class BookViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public double Rating { get; set; }
    public BookStatus Status { get; set; }
    public string CreatorId { get; set; }
    public IEnumerable<AuthorViewModel> Authors { get; set; }
}