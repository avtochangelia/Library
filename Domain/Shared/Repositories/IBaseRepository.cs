using System.Linq.Expressions;

namespace Domain.Shared.Repositories;

public interface IBaseRepository<TAggregateRoot> where TAggregateRoot : class
{
    Task<TAggregateRoot?> OfIdAsync(Guid id);

    Task<TAggregateRoot?> OfIdAsync(string id);

    IQueryable<TAggregateRoot> Query(Expression<Func<TAggregateRoot, bool>>? expression = default);

    Task<List<TAggregateRoot>> QueryAsync(Expression<Func<TAggregateRoot, bool>>? expression = default);

    void Delete(TAggregateRoot aggregateRoot);

    Task InsertAsync(TAggregateRoot aggregateRoot);

    void Update(TAggregateRoot aggregateRoot);
}