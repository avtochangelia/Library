using MediatR;

namespace Application.BookManagement.Commands.BringInBook;

public class BringInBook : IRequest
{
    public Guid BookId { get; set; }
}