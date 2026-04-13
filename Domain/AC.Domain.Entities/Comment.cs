using AC.Domain.Entities.Entities;
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
    public Comment(CommentContent content, Guid authorId, Guid caseId) : base()
    {
        Content = content;
        AuthorId = authorId;
        CaseId = caseId;
    }
}