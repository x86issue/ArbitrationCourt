using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class Comment : Entity<Guid>
{
    public CommentContent Content { get; private set; } = null!;
    public Plaintiff? AuthorPlaintiff { get; private set; }
    public Defendant? AuthorDefendant { get; private set; }
    public Arbitrator? AuthorArbitrator { get; private set; }
    public Guid AuthorId { get; private set; }
    public Case CaseAssigned { get; private set; } = null!;

    protected Comment()
    {
    }

    public Comment(Plaintiff plaintiff, Case _case, CommentContent content) : base(Guid.NewGuid())
    {
        Content = content ?? throw new ArgumentNullException(nameof(content));
        if (plaintiff.Id == Guid.Empty) throw new ArgumentException("Author cannot be empty.", nameof(plaintiff.Id));
        if (_case.Id == Guid.Empty) throw new ArgumentException("Case cannot be empty.", nameof(_case.Id));
        CaseAssigned = _case;
        AuthorPlaintiff = plaintiff;
        AuthorId = plaintiff.Id;
    }

    public Comment(Defendant defendant, Case _case, CommentContent content) : base(Guid.NewGuid())
    {
        Content = content ?? throw new ArgumentNullException(nameof(content));
        if (defendant.Id == Guid.Empty) throw new ArgumentException("AuthorId cannot be empty.", nameof(defendant.Id));
        if (_case.Id == Guid.Empty) throw new ArgumentException("CaseId cannot be empty.", nameof(_case.Id));
        CaseAssigned = _case;
        AuthorDefendant = defendant;
        AuthorId = defendant.Id;
    }

    public Comment(Arbitrator arbitrator, Case _case, CommentContent content) : base(Guid.NewGuid())
    {
        Content = content ?? throw new ArgumentNullException(nameof(content));
        if (arbitrator.Id == Guid.Empty) throw new ArgumentException("AuthorId cannot be empty.", nameof(arbitrator.Id));
        if (_case.Id == Guid.Empty) throw new ArgumentException("CaseId cannot be empty.", nameof(_case.Id));
        CaseAssigned = _case;
        AuthorArbitrator = arbitrator;
        AuthorId = arbitrator.Id;
    }
}
