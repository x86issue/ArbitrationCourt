using AC.Domain.Entities;

namespace AC.Domain.Repositories.Abstractions;

public interface ICaseRepository : IRepository<Case, Guid>
{
    Task<Case?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);
}
