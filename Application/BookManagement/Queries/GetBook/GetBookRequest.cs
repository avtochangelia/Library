using MediatR;

namespace Application.BookManagement.Queries.GetBook;

public class GetBookRequest : IRequest<GetBookResponse>
{
    public Guid BookId { get; set; }
}