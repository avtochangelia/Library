#nullable disable

using Library.App.Models.ViewModels.Books;

namespace Library.App.Models.ViewModels;

public abstract class BasePagedResult
{
    public string SearchQuery { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int TotalPages => (int)Math.Ceiling((decimal)Total / PageSize);

    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}
