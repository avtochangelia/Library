using Domain.BookManagement;
using MediatR;

namespace Application.AuthorManagement.Commands.CreateAuthor;

public record CreateAuthor(string FirstName, string LastName, DateTime DateOfBirth) : IRequest;