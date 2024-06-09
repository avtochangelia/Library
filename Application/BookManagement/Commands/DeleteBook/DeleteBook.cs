using MediatR;

namespace Application.BookManagement.Commands.DeleteBook;

public class DeleteBook : IRequest
{
    public Guid BookId { get; set; }
}