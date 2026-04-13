using AC.Domain.Entities.Enums;
using AC.Domain.ValueObjects;


namespace AC.Domain.Entities.Entities;

public class SettlementProposal : Entity
{
    // ПОЛЯ
    public Guid CaseId { get; private set; }
    public Guid ProposerId { get; private set; }
    public ProposalContent Content { get; private set; }
    public ProposalStatus Status { get; private set; } = ProposalStatus.Created;

    // МЕТОДЫ


    // КОНСТРУКТОРЫ
    protected SettlementProposal() { }

    public SettlementProposal(Guid caseId, Guid proposerId, ProposalContent content) : base()
    {
        CaseId = caseId;
        ProposerId = proposerId;
        Content = content;
    }
}