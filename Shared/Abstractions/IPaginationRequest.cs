namespace Shared.Abstractions;

public interface IPaginationRequest
{
    public int? Page { get; set; }
    public int? PageSize { get; set; }
}