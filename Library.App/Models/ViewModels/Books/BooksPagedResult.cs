#nullable disable

using Library.App.Models.ViewModels.Authors;

namespace Library.App.Models.ViewModels.Books;

public class BooksPagedResult : BasePagedResult
{
    public IEnumerable<BookViewModel> Books { get; set; }
    public IEnumerable<AuthorViewModel> AllAuthors { get; set; }
}