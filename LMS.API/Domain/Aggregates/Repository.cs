using LMS.Domain.Entities;
using LMS.Infrastructure;

namespace LMS.Domain.Aggregates;

public class
    Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, IAggregateRoot
{
    private readonly LMSDbContext _dbContext;

    public Repository(LMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbContext.AddAsync(entity);
    }

    public async Task<TEntity> FindByIdAsync(int id)
    {
        return await _dbContext.FindAsync<TEntity>(id);
    }

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        _dbContext.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}