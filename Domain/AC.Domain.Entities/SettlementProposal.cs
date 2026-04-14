using AC.Domain.Entities.Enums;
using AC.Domain.ValueObjects;


namespace AC.Domain.Entities;

public class SettlementProposal : Entity
{
    // ПОЛЯ
    public Guid CaseId { get; private set; }
    public Guid ProposerId { get; private set; }
    public ProposalContent Content { get; private set; }
    public ProposalStatus Status { get; private set; } = ProposalStatus.Created;

    // МЕТОДЫ

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

    // КОНСТРУКТОРЫ
    public SettlementProposal() { }

    public SettlementProposal(Guid proposerId, Guid caseId, ProposalContent content) : base()
    {
        if(proposerId == Guid.Empty) throw new ArgumentException("ProposerId cannot be empty.", nameof(proposerId));
        if(caseId == Guid.Empty) throw new ArgumentException("CaseId cannot be empty.", nameof(caseId));

        CaseId = caseId;
        ProposerId = proposerId;
        Content = content;
    }
}