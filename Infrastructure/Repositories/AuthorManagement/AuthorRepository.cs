using Domain.AuthorManagement;
using Domain.AuthorManagement.Repositories;
using Infrastructure.DataAccess;

namespace Infrastructure.Repositories.AuthorManagement;

public class AuthorRepository(EFDbContext dbContext) : EFBaseRepository<EFDbContext, Author>(dbContext), IAuthorRepository
{
}