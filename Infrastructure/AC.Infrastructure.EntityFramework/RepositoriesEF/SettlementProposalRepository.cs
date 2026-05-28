using AC.Domain.Entities;
using AC.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AC.Infrastructure.EntityFramework.RepositoriesEF;

public class SettlementProposalRepository(ApplicationDbContext dbContext)
    : EFRepository<SettlementProposal, Guid>(dbContext), ISettlementProposalRepository
{
    public async Task<IReadOnlyList<SettlementProposal>> GetByCaseIdAsync(Guid caseId, CancellationToken cancellationToken = default)
    {
        return await DbContext.SettlementProposals
            .AsNoTracking()
            .Where(proposal => EF.Property<Guid>(proposal, "CaseId") == caseId)
            .ToListAsync(cancellationToken);
    }
}
