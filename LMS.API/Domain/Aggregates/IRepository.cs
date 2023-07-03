namespace LMS.Domain.Aggregates;

public interface IRepository<T> where T : IAggregateRoot
{
    Task AddAsync(T entity);
    Task<T> FindByIdAsync(int id);
    void Update(T entity);
    void Remove(T entity);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}