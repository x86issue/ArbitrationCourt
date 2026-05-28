using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class Claim : Entity<Guid>
{
    public ClaimContent Content { get; private set; } = null!;
    public Plaintiff Plaintiff { get; private set; } = null!;
    public Defendant Defendant { get; private set; } = null!;

    protected Claim()
    {
    }

    public Claim(DateTime created, Plaintiff plaintiff, Defendant defendant, ClaimContent content)
        : this(Guid.NewGuid(), created, plaintiff, defendant, content)
    {
    }

    protected Claim(Guid id, DateTime created, Plaintiff plaintiff, Defendant defendant, ClaimContent content) : base(id)
    {
        Plaintiff = plaintiff ?? throw new ArgumentNullException(nameof(plaintiff));
        Defendant = defendant ?? throw new ArgumentNullException(nameof(defendant));
        Content = content ?? throw new ArgumentNullException(nameof(content));
    }
}
