using MediatR;

namespace Application.BookManagement.Commands.TakeOutBook;

public class TakeOutBook : IRequest
{
    public Guid BookId { get; set; }
}