using AC.Domain.Entities.Enums;
using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class SettlementProposal : Entity<Guid>
{
    public Case CaseAssigned { get; private set; } = null!;
    public Defendant Defendant { get; private set; } = null!;
    public ProposalContent Content { get; private set; } = null!;
    public ProposalStatus Status { get; private set; } = ProposalStatus.Created;

    protected SettlementProposal()
    {
    }

    public void Accept()
    {
        if (Status != ProposalStatus.Created)
            throw new InvalidOperationException("Proposal can only be accepted if it is in 'Created' status.");
        Status = ProposalStatus.Accepted;
    }

    public void Reject()
    {
        if (Status != ProposalStatus.Created)
            throw new InvalidOperationException("Proposal can only be rejected if it is in 'Created' status.");
        Status = ProposalStatus.Rejected;
    }

    public SettlementProposal(DateTime created, Defendant defendant, Case _case, ProposalContent content)
        : this(Guid.NewGuid(), created, defendant, _case, content)
    {
    }

    protected SettlementProposal(Guid id, DateTime created, Defendant defendant, Case _case, ProposalContent content) : base(id)
    {
        Defendant = defendant ?? throw new ArgumentNullException(nameof(defendant));
        Content = content ?? throw new ArgumentNullException(nameof(content));
        CaseAssigned = _case ?? throw new ArgumentNullException(nameof(_case));
    }
}
