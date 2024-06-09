using MediatR;

namespace Application.BookManagement.Commands.CreateBook;

public record CreateBook(string Title, string Description, string Image, double Rating, IEnumerable<Guid> AuthorIds) : IRequest;