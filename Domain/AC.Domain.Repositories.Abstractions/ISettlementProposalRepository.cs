using AC.Domain.Entities;

namespace AC.Domain.Repositories.Abstractions;

public interface ISettlementProposalRepository : IRepository<SettlementProposal, Guid>
{
    Task<IReadOnlyList<SettlementProposal>> GetByCaseIdAsync(Guid caseId, CancellationToken cancellationToken = default);
}
