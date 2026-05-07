using AC.Domain.Entities.Enums;
using AC.Domain.Entities.Exceptions;
using AC.Domain.ValueObjects;



namespace AC.Domain.Entities;

public class Case : Entity
{
    // ПОЛЯ
    public CaseTitle Title { get; private set; }
    public CaseDescription Description { get; private set; }



    private ICollection<Comment> _comments = new List<Comment>();
    private ICollection<SettlementProposal> _proposals = new List<SettlementProposal>();
    private ICollection<CourtRule> _rules = new List<CourtRule>();

    public IReadOnlyCollection<Comment> Comments => _comments.ToList();
    public IReadOnlyCollection<SettlementProposal> Proposals => _proposals.ToList();
    public IReadOnlyCollection<CourtRule> Rules => _rules.ToList();


    public Plaintiff Plaintiff { get; }
    public Defendant Defendant { get; }
    public Arbitrator? Arbitrator { get; private set;/* подумать над возможностью изменять судью */ }

    public CaseStatus Status { get; private set; } = CaseStatus.Opened;
    public Claim? Claim { get; private set; }
    public Verdict? Verdict { get; private set; }

    public DateTime? ClosedAt { get; private set; }


    public bool isActive() // активно ли дело
    {
        if (Status == CaseStatus.Opened || Status == CaseStatus.InProgress) return true;

        return false;
    }

 
    public bool ArbitratorIsParticipant(Arbitrator arbitrator)
    {
        if (Arbitrator == null || Arbitrator != arbitrator)
        {
            throw new CommentNotAllowedException();
        }

        return true;
    }

    public bool DefendantIsParticipant(Defendant defendant)
    {
        if (Defendant != defendant)
        {
            throw new CommentNotAllowedException();
        }

        return true;
    }

    public bool PlaintiffIsParticipant(Plaintiff plaintiff)
    {
        if (Plaintiff != plaintiff)
        {
            throw new CommentNotAllowedException();
        }

        return true;
    }


    public bool IsArbitrator(Arbitrator arbitrator) // является ли арбитром
    {
        if (Arbitrator == arbitrator) return true;
        else return false;
    }

    public bool HasActiveProposal()
    {
        if(Proposals.Any(p => p.Status == ProposalStatus.Created)) return true;
        else return false;
    }

    // МЕТОДЫ

    // МЕТОД-ЗАГЛУШКА ПОКА НЕТ НУМИРОВАННОГО СПИСКА СУДЕЙ С ВОЗМОЖНОСТЬЮ ОДОБРЕНИЯ С ОБЕИХ СТОРОН
    public bool AssignArbitrator(Arbitrator arbitrator) // назначить арбитра
    {
        if(Status == CaseStatus.ClosedByVerdict) throw new InvalidOperationException("Невозможно назначить арбитра для дела, которое закрыто вердиктом.");

        if (Arbitrator != null) throw new InvalidOperationException("Арбитр уже назначен для этого дела.");

        Arbitrator = arbitrator;
        Status = CaseStatus.InProgress;
        return true;
    }

    public bool AddRule(CourtRule rule)
    {
        if (rule is null)
            throw new ArgumentNullException(nameof(rule));

        if (_rules.Any(r => r.Id == rule.Id))
            throw new InvalidOperationException("Это правило уже добавлено к делу.");

        _rules.Add(rule);
        return true;
    }

    public bool RemoveRule(CourtRule rule)
    {
        if (rule is null)
            throw new ArgumentNullException(nameof(rule));

        var existingRule = _rules.FirstOrDefault(r => r.Id == rule.Id);

        if (existingRule is null)
            throw new InvalidOperationException("Это правило не привязано к делу.");

        _rules.Remove(existingRule);
        return true;
    }

    public bool HasRule(CourtRule rule)
    {
        if (rule is null)
            throw new ArgumentNullException(nameof(rule));

        return _rules.Any(r => r.Id == rule.Id);
    }


    public Comment AddCommentByPlaintiff(Plaintiff plaintiff, CommentContent content) // добавить комментарий
    {
        PlaintiffIsParticipant(plaintiff);
        var comment = new Comment(plaintiff, this.Id, content);
        _comments.Add(comment);
        return comment;

    }

    public Comment AddCommentByDefendant(Defendant defendant, CommentContent content) // добавить комментарий
    {
        DefendantIsParticipant(defendant);
        var comment = new Comment(defendant, this.Id, content);
        _comments.Add(comment);
        return comment;

    }

    public Comment AddCommentByArbitrator(Arbitrator arbitrator, CommentContent content) // добавить комментарий
    {
        ArbitratorIsParticipant(arbitrator);
        var comment = new Comment(arbitrator, this.Id, content);
        _comments.Add(comment);
        return comment;

    }


    // ПОДУМАТЬ МЕТОДЫ СВЕРХУ ВОЗМОЖНО МОЖНО ОБЪЕДИНИТЬ И ВЫЗЫВАТЬ ЧЕРЕЗ ОДНУ КОМАНДУ
    public SettlementProposal CreateProposal(Defendant defendant, ProposalContent content) // создать предложение по урегулированию
    {
        if (Defendant != defendant) throw new InvalidOperationException("Пользователь не может создавать предложения для этого дела.");
        if (Status != CaseStatus.InProgress && Status != CaseStatus.Opened) throw new InvalidOperationException("Предложения могут быть созданы только для дел в процессе.");
        var proposal = new SettlementProposal(defendant, this.Id, content);
        _proposals.Add(proposal);
        return proposal;
    }

    public Verdict IssueVerdict(Arbitrator arbitrator, VerdictContent content) // вынести вердикт
    {
        if (Arbitrator != arbitrator) throw new InvalidOperationException("Данный арбитр не может выносить вердикт для этого дела.");
        if (Status != CaseStatus.InProgress && Status != CaseStatus.Opened) throw new InvalidOperationException("Вердикт может быть вынесен только для дела в процессе.");
        if (content == null) throw new ArgumentNullException(nameof(content));
        if (this.Status == CaseStatus.ClosedByProposal || this.Status == CaseStatus.ClosedByVerdict)
                 throw new InvalidOperationException("Невозможно вынести вердикт для дела, которое закрыто предложением по урегулированию.");

        Verdict = new Verdict(arbitrator, Id, content);
        Status = CaseStatus.ClosedByVerdict;
        ClosedAt = DateTime.UtcNow;
        return Verdict;
    }

    public Claim CreateClaim(ClaimContent content) // создать иск
    {
        if (Status != CaseStatus.Opened) throw new InvalidOperationException("Иск может быть создан только для открытого дела.");
        Claim = new Claim(Plaintiff, Defendant, content);
        return Claim;

    }

    public bool AcceptProposal(Plaintiff plaintiff, SettlementProposal proposal) // принять предложение по урегулированию
    {
        if(Plaintiff != plaintiff) throw new InvalidOperationException("Пользователь не может принимать предложения для этого дела.");
        if (Status != CaseStatus.InProgress && Status != CaseStatus.Opened) throw new InvalidOperationException("Предложение может быть принято только для дела в процессе.");
        Status = CaseStatus.ClosedByProposal;
        proposal.Accept();
        ClosedAt = DateTime.UtcNow;
        return true;
    }

    public bool RejectProposal(Plaintiff plaintiff, SettlementProposal proposal) // отклонить предложение по урегулированию
    {
        if (plaintiff.Id != Plaintiff.Id) throw new InvalidOperationException("Пользователь не может отклонять предложения для этого дела.");
        if (proposal.Status != ProposalStatus.Created) throw new InvalidOperationException("Предложение уже обработано");
        if (Status != CaseStatus.InProgress && Status != CaseStatus.Opened) throw new InvalidOperationException("Предложение может быть отклонено только для дела в процессе.");
        proposal.Reject();
        return true;
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
