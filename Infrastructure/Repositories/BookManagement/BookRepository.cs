using Domain.BookManagement;
using Domain.BookManagement.Repositories;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories.BookManagement;

public class BookRepository(EFDbContext dbContext) : EFBaseRepository<EFDbContext, Book>(dbContext), IBookRepository
{
}