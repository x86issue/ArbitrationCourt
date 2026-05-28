using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class Verdict : Entity<Guid>
{
    public Case CaseAssigned { get; private set; } = null!;
    public Arbitrator Arbitrator { get; private set; } = null!;
    public VerdictContent Content { get; private set; } = null!;

    protected Verdict()
    {
    }

    public Verdict(DateTime created, Arbitrator arbitrator, Case _case, VerdictContent content)
        : this(Guid.NewGuid(), created, arbitrator, _case, content)
    {
    }

    protected Verdict(Guid id, DateTime created, Arbitrator arbitrator, Case _case, VerdictContent content) : base(id)
    {
        Arbitrator = arbitrator ?? throw new ArgumentNullException(nameof(arbitrator));
        Content = content ?? throw new ArgumentNullException(nameof(content));
        CaseAssigned = _case ?? throw new ArgumentNullException(nameof(_case));
    }
}
