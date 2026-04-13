using AC.Domain.Entities.Entities;
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

    public Verdict(Guid caseId, Arbitrator arbitrator, VerdictContent content) : base()
    {
        CaseId = caseId;
        Arbitrator = arbitrator;
        Content = content;
    }
}