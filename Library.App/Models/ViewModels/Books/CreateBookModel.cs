#nullable disable

using Library.App.Models.ViewModels.Authors;

namespace Library.App.Models.ViewModels.Books;

public class CreateBookModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public double Rating { get; set; }
    public IEnumerable<Guid> AuthorIds { get; set; }
}