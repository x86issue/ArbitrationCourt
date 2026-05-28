using AC.Domain.Entities;
using AC.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AC.Infrastructure.EntityFramework.RepositoriesEF;

public class EFRepository<TEntity, TId>(ApplicationDbContext dbContext) : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : struct, IEquatable<TId>
{
    protected ApplicationDbContext DbContext { get; } = dbContext;
    protected DbSet<TEntity> Entities => DbContext.Set<TEntity>();

    public virtual async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        return await Entities.FindAsync([id], cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Entities.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }

    public virtual void Update(TEntity entity)
    {
        Entities.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return DbContext.SaveChangesAsync(cancellationToken);
    }
}
