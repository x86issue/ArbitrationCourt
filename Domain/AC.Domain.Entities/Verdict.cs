using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class Verdict : Entity
{
    // ПОЛЯ
    public Guid CaseId { get; private set; }
    public Arbitrator Arbitrator { get; private set; }
    public VerdictContent Content { get; private set; }


    // МЕТОДЫ 



    // КОНСТРУКТОРЫ
    protected Verdict() { }

    public Verdict(Arbitrator arbitrator, Guid caseId, VerdictContent content) : base()
    {
        if(caseId == Guid.Empty) throw new ArgumentException("CaseId cannot be empty.", nameof(caseId));
        if(arbitrator == null) throw new ArgumentNullException(nameof(arbitrator));
        CaseId = caseId;
        Arbitrator = arbitrator;
        Content = content;
    }
}