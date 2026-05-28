using AC.Domain.Entities;
using AC.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AC.Infrastructure.EntityFramework.RepositoriesEF;

public class CaseRepository(ApplicationDbContext dbContext) : EFRepository<Case, Guid>(dbContext), ICaseRepository
{
    public async Task<Case?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext.Cases
            .Include(@case => @case.Plaintiff)
            .Include(@case => @case.Defendant)
            .Include(@case => @case.Arbitrator)
            .Include(@case => @case.Claim)
            .Include(@case => @case.Verdict)
            .Include(@case => @case.Comments)
            .Include(@case => @case.Proposals)
            .Include(@case => @case.Rules)
            .FirstOrDefaultAsync(@case => @case.Id == id, cancellationToken);
    }
}
