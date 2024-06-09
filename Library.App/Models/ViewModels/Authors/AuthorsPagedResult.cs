#nullable disable

namespace Library.App.Models.ViewModels.Authors;

public class AuthorsPagedResult : BasePagedResult
{
    public IEnumerable<AuthorViewModel> Authors { get; set; }
}