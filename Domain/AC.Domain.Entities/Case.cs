using AC.Domain.Entities.Enums;
using AC.Domain.Entities.Exceptions;
using AC.Domain.ValueObjects;



namespace AC.Domain.Entities;

public class Case : Entity
{
    // ПОЛЯ
    public CaseTitle Title { get; private set; }
    public CaseDescription Description { get; private set; }



    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Comment> Comments => _comments;

    public Plaintiff Plaintiff { get; }
    public Defendant Defendant { get; }
    public Arbitrator? Arbitrator { get; /* подумать над возможностью изменять судью */ }

    public CaseStatus Status { get; private set; } = CaseStatus.Opened;

    public DateTime? ClosedAt { get; private set; }


    // ПРОВЕРКИ
    public bool isActive() // активно ли дело
    {
        return Status == CaseStatus.Opened || Status == CaseStatus.InProgress;
    }

    // ДЖБТ СКАЗАЛ ПЕРЕПИСАТЬ ПРОВЕРКУ canComment, назвав меня дураком за неправильную логику DDD. 
    public void EnsureCanComment(Guid userId)
    {
        if (!(Plaintiff.Id == userId || Defendant.Id == userId || Arbitrator?.Id == userId))
            throw new CommentNotAllowedException();
    }

    public bool IsParticipant(Guid userId) // участник дела
    {
        if(Plaintiff.Id == userId || Defendant.Id == userId) return true;
        else return false;
    }

    public bool IsArbitrator(Guid userId) // является ли арбитром
    {
        if (Arbitrator?.Id == userId) return true;
        else return false;
    }

    public bool HasActiveProposal()
    {
        return Status == CaseStatus.InProgress;
    }

    // МЕТОДЫ

    // МЕТОД-ЗАГЛУШКА ПОКА НЕТ НУМИРОВАННОГО СПИСКА СУДЕЙ С ВОЗМОЖНОСТЬЮ ОДОБРЕНИЯ С ОБЕИХ СТОРОН
    public void AssignArbitrator(Arbitrator arbitrator) // назначить арбитра
    {
        if(Status == CaseStatus.ClosedByVerdict) throw new InvalidOperationException("Невозможно назначить арбитра для дела, которое закрыто вердиктом.");

        if (Arbitrator != null) throw new InvalidOperationException("Арбитр уже назначен для этого дела.");

        Arbitrator = arbitrator;
        Status = CaseStatus.InProgress;
    }

    public Comment AddComment(Guid authorid, CommentContent content) // добавить комментарий
    {
        EnsureCanComment(authorid);

        var comment = new Comment(authorid, this.Id, content);

        _comments.Add(comment);

        return comment;
    }

    public SettlementProposal CreateProposal(Guid authorId, ProposalContent content) // создать предложение по урегулированию
    {
        if (!IsParticipant(Defendant.Id)) throw new InvalidOperationException("Пользователь не может создавать предложения для этого дела.");
        if (Status != CaseStatus.InProgress || Status != CaseStatus.Opened) throw new InvalidOperationException("Предложения могут быть созданы только для дел в процессе.");
        return new SettlementProposal(authorId, this.Id, content);
    }

    public Verdict IssueVerdict(Arbitrator arbitrator, VerdictContent content) // вынести вердикт
    {
        if (!IsArbitrator(arbitrator.Id)) throw new InvalidOperationException("Данный арбитр не может выносить вердикт для этого дела.");
        if (Status != CaseStatus.InProgress || Status != CaseStatus.Opened) throw new InvalidOperationException("Вердикт может быть вынесен только для дела в процессе.");
        if (content == null) throw new ArgumentNullException(nameof(content));
        if (this.Status == CaseStatus.ClosedByProposal || this.Status == CaseStatus.ClosedByVerdict)
                 throw new InvalidOperationException("Невозможно вынести вердикт для дела, которое закрыто предложением по урегулированию.");
        Status = CaseStatus.ClosedByVerdict;
        ClosedAt = DateTime.UtcNow;
        return new Verdict(arbitrator, this.Id, content);
    }

    public Claim CreateClaim(ClaimContent content) // создать иск
    {
        if (Status != CaseStatus.Opened) throw new InvalidOperationException("Иск может быть создан только для открытого дела.");
        return new Claim(Plaintiff, Defendant, content);
    }

    public void AcceptProposal(Plaintiff plaintiff, SettlementProposal proposal) // принять предложение по урегулированию
    {
        if(plaintiff.Id != Plaintiff.Id) throw new InvalidOperationException("Пользователь не может принимать предложения для этого дела.");
        if (Status != CaseStatus.InProgress || Status != CaseStatus.Opened) throw new InvalidOperationException("Предложение может быть принято только для дела в процессе.");
        Status = CaseStatus.ClosedByProposal;
        proposal.Accept();
        ClosedAt = DateTime.UtcNow;
    }

    public void RejectProposal(Plaintiff plaintiff, SettlementProposal proposal) // отклонить предложение по урегулированию
    {
        if (plaintiff.Id != Plaintiff.Id) throw new InvalidOperationException("Пользователь не может отклонять предложения для этого дела.");
        if (proposal.Status != ProposalStatus.Created) throw new InvalidOperationException("Предложение уже обработано");
        if (Status != CaseStatus.InProgress || Status != CaseStatus.Opened) throw new InvalidOperationException("Предложение может быть отклонено только для дела в процессе.");
        proposal.Reject();
    }

    // ======================================================================================================================================================
    // ПРОПИСАТЬ контракты IArbitratorRepository, ICommentRepository и ICaseRepository для получения списка арбитров, всех аргументов и дел 
    // ИДИ ПОДУМАТЬ как это сделать иначе
    // ======================================================================================================================================================



    // ======================================================================================================================================================
    // ПЕРЕГРУЗИТЬ ОПЕРАТОРЫ .ToString() для удобства отображения информации о деле, комментариях, предложениях и вердиктах
    // ======================================================================================================================================================




    // КОНСТРУКТОРЫ
    protected Case() { }
    
    public Case(Plaintiff plaintiff, Defendant defendant, CaseTitle title, CaseDescription description) : base()
    {
        Title = title;
        Description = description;
        Plaintiff = plaintiff ?? throw new ArgumentNullException(nameof(plaintiff));
        Defendant = defendant ?? throw new ArgumentNullException(nameof(defendant));
    }


}
