using Domain.Shared;

namespace Infrastructure.DataAccess;

public class UnitOfWork(EFDbContext context) : IUnitOfWork
{
    private readonly EFDbContext _context = context;

    public async Task SaveAsync()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        await _context.SaveChangesAsync();

        await transaction.CommitAsync();
    }
}