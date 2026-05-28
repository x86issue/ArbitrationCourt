using AC.Domain.Entities;

namespace AC.Domain.Repositories.Abstractions;

public interface IArbitratorRepository : IRepository<Arbitrator, Guid>
{
    Task<IReadOnlyList<Arbitrator>> GetActiveAsync(CancellationToken cancellationToken = default);
}
