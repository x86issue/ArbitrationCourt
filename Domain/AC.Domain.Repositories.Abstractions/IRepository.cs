using AC.Domain.Entities;

namespace AC.Domain.Repositories.Abstractions;

public interface IRepository<TEntity, in TId>
    where TEntity : Entity<TId>
    where TId : struct, IEquatable<TId>
{
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
