using AC.Domain.Entities;
using AC.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AC.Infrastructure.EntityFramework.RepositoriesEF;

public class ArbitratorRepository(ApplicationDbContext dbContext) : EFRepository<Arbitrator, Guid>(dbContext), IArbitratorRepository
{
    public async Task<IReadOnlyList<Arbitrator>> GetActiveAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Arbitrators
            .AsNoTracking()
            .Where(arbitrator => arbitrator.IsActived)
            .ToListAsync(cancellationToken);
    }
}
