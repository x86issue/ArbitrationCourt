using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class Comment : Entity
{
    // ПОЛЯ
    public CommentContent Content { get; private set; }
    
    public Guid AuthorId { get; private set; }
    public Guid CaseId { get; private set; }
    

    
    // МЕТОДЫ
    
    
    
    // КОНСТРУКТОРЫ
    protected Comment() { }
    public Comment(Plaintiff plaintiff, Guid caseId, CommentContent content) : base()
    {
        Content = content;
        if(plaintiff.Id == Guid.Empty) throw new ArgumentException("AuthorId cannot be empty.", nameof(plaintiff.Id));
        if(caseId == Guid.Empty) throw new ArgumentException("CaseId cannot be empty.", nameof(caseId));
        CaseId = caseId;
        AuthorId = plaintiff.Id;
    }

    public Comment(Defendant defendant, Guid caseId, CommentContent content) : base()
    {
        Content = content;
        if (defendant.Id == Guid.Empty) throw new ArgumentException("AuthorId cannot be empty.", nameof(defendant.Id));
        if (caseId == Guid.Empty) throw new ArgumentException("CaseId cannot be empty.", nameof(caseId));
        CaseId = caseId;
        AuthorId = defendant.Id;
    }

    public Comment(Arbitrator arbitrator, Guid caseId, CommentContent content) : base()
    {
        Content = content;
        if (arbitrator.Id == Guid.Empty) throw new ArgumentException("AuthorId cannot be empty.", nameof(arbitrator.Id));
        if (caseId == Guid.Empty) throw new ArgumentException("CaseId cannot be empty.", nameof(caseId));
        CaseId = caseId;
        AuthorId = arbitrator.Id;
    }
}