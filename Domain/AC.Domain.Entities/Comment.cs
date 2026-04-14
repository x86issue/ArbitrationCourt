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
    public Comment(Guid authorId, Guid caseId, CommentContent content) : base()
    {
        Content = content;
        if(authorId == Guid.Empty) throw new ArgumentException("AuthorId cannot be empty.", nameof(authorId));
        if(caseId == Guid.Empty) throw new ArgumentException("CaseId cannot be empty.", nameof(caseId));
        CaseId = caseId;
        AuthorId = authorId;
    }
}