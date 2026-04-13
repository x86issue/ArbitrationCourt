using AC.Domain.ValueObjects;
using AC.Domain.Entities.Enums;
using AC.Domain.Entities.Entities;


namespace AC.Domain.Entities;

public class Case : Entity
{
    // ПОЛЯ
    public CaseTitle Title { get; private set; }
    public CaseDescription Description { get; private set; }

    public Plaintiff Plaintiff { get; private set; }
    public Defendant Defendant { get; private set; }
    public Arbitrator? Arbitrator { get; private set; }

    public CaseStatus Status { get; } = CaseStatus.Opened;

    public DateTime? ClosedAt { get; private set; }

    // МЕТОДЫ



    // КОНСТРУКТОРЫ
    protected Case() { }

    public Case(CaseTitle title, CaseDescription description, Plaintiff plaintiff, Defendant defendant)
    {
        Title = title;
        Description = description;
        Plaintiff = plaintiff;
        Defendant = defendant;
    }
}
