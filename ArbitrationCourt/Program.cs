using AC.Domain.Entities;
using AC.Domain.Entities.Enums;
using AC.Domain.ValueObjects;

namespace ArbitrationCourt;

internal class Program
{
    private static readonly List<Plaintiff> Plaintiffs = new();
    private static readonly List<Defendant> Defendants = new();
    private static readonly List<Arbitrator> Arbitrators = new();
    private static readonly List<Case> Cases = new();
    private static readonly List<CourtRule> CourtRules = new();

    private static Case? CurrentCase;

    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        SeedData();

        while (true)
        {
            PrintMenu();

            var command = ReadString("Выберите действие");

            try
            {
                Console.WriteLine();

                switch (command)
                {
                    case "1":
                        CreatePlaintiff();
                        break;

                    case "2":
                        CreateDefendant();
                        break;

                    case "3":
                        CreateArbitrator();
                        break;

                    case "4":
                        CreateCase();
                        break;

                    case "5":
                        SelectCurrentCase();
                        break;

                    case "6":
                        CreateClaim();
                        break;

                    case "7":
                        AssignArbitrator();
                        break;

                    case "8":
                        AddComment();
                        break;

                    case "9":
                        CreateProposal();
                        break;

                    case "10":
                        AcceptProposal();
                        break;

                    case "11":
                        RejectProposal();
                        break;

                    case "12":
                        IssueVerdict();
                        break;

                    case "13":
                        ChangePlaintiffName();
                        break;

                    case "14":
                        ChangeDefendantName();
                        break;

                    case "15":
                        ChangeArbitratorData();
                        break;

                    case "16":
                        ShowCurrentCaseDetails();
                        break;

                    case "17":
                        ShowAllObjects();
                        break;

                    case "18":
                        CreateCourtRule();
                        break;

                    case "19":
                        ShowAllCourtRules();
                        break;

                    case "20":
                        AddRuleToCurrentCase();
                        break;

                    case "21":
                        RemoveRuleFromCurrentCase();
                        break;

                    case "22":
                        ShowCurrentCaseRules();
                        break;

                    case "0":
                        Console.WriteLine("Выход.");
                        return;

                    default:
                        PrintWarning("Неизвестная команда.");
                        break;
                }
            }
            catch (Exception ex)
            {
                PrintError(ex);
            }

            Pause();
        }
    }

    private static void PrintMenu()
    {
        Console.Clear();

        Console.WriteLine("==================================================");
        Console.WriteLine("              ARBITRATION COURT");
        Console.WriteLine("==================================================");



        if (CurrentCase is null)
        {
            Console.WriteLine("Текущее дело: не выбрано");
        }
        else
        {
            Console.WriteLine($"Текущее дело: {CurrentCase.Title} | Статус: {CurrentCase.Status}");
        }

        Console.WriteLine("==================================================");
        Console.WriteLine("1.  Создать истца");
        Console.WriteLine("2.  Создать ответчика");
        Console.WriteLine("3.  Создать арбитра");
        Console.WriteLine("4.  Создать дело");
        Console.WriteLine("5.  Выбрать текущее дело");
        Console.WriteLine("6.  Создать иск для текущего дела");
        Console.WriteLine("7.  Назначить арбитра на текущее дело");
        Console.WriteLine("8.  Добавить комментарий к текущему делу");
        Console.WriteLine("9.  Создать предложение об урегулировании");
        Console.WriteLine("10. Принять предложение");
        Console.WriteLine("11. Отклонить предложение");
        Console.WriteLine("12. Вынести вердикт");
        Console.WriteLine("13. Изменить имя истца");
        Console.WriteLine("14. Изменить имя ответчика");
        Console.WriteLine("15. Изменить данные арбитра");
        Console.WriteLine("16. Показать подробности текущего дела");
        Console.WriteLine("17. Показать все объекты");
        Console.WriteLine("18. Создать правило суда");
        Console.WriteLine("19. Показать все правила суда");
        Console.WriteLine("20. Добавить правило к текущему делу");
        Console.WriteLine("21. Удалить правило из текущего дела");
        Console.WriteLine("22. Показать правила текущего дела");
        Console.WriteLine("0.  Выход");
        Console.WriteLine("==================================================");
    }

    // ============================================================
    // CREATE
    // ============================================================


    private static void CreatePlaintiff()
    {
        Console.WriteLine("Создание истца");

        var firstName = ReadString("Введите имя истца");
        var lastName = ReadString("Введите фамилию истца");

        var plaintiff = new Plaintiff(
            new FirstName(firstName),
            new LastName(lastName)
        );

        Plaintiffs.Add(plaintiff);

        PrintSuccess("Истец создан.");
        PrintPlaintiff(plaintiff, Plaintiffs.Count - 1);
    }

    private static void CreateDefendant()
    {
        Console.WriteLine("Создание ответчика");

        var firstName = ReadString("Введите имя ответчика");
        var lastName = ReadString("Введите фамилию ответчика");

        var defendant = new Defendant(
            new FirstName(firstName),
            new LastName(lastName)
        );

        Defendants.Add(defendant);

        PrintSuccess("Ответчик создан.");
        PrintDefendant(defendant, Defendants.Count - 1);
    }

    private static void CreateArbitrator()
    {
        Console.WriteLine("Создание арбитра");

        var firstName = ReadString("Введите имя арбитра");
        var lastName = ReadString("Введите фамилию арбитра");
        var bio = ReadString("Введите описание / биографию арбитра");
        var experience = ReadInt("Введите опыт арбитра в годах");

        var arbitrator = new Arbitrator(
            new FirstName(firstName),
            new LastName(lastName),
            bio,
            experience
        );

        Arbitrators.Add(arbitrator);

        PrintSuccess("Арбитр создан.");
        PrintArbitrator(arbitrator, Arbitrators.Count - 1);
    }

    private static void CreateCase()
    {
        Console.WriteLine("Создание дела");

        var plaintiff = SelectPlaintiff();
        var defendant = SelectDefendant();

        var title = ReadString("Введите название дела");
        var description = ReadString("Введите описание дела");

        var courtCase = new Case(
            plaintiff,
            defendant,
            new CaseTitle(title),
            new CaseDescription(description)
        );

        Cases.Add(courtCase);
        CurrentCase = courtCase;

        PrintSuccess("Дело создано и выбрано как текущее.");
        PrintCase(courtCase, Cases.Count - 1);
    }

    private static void CreateClaim()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("Создание иска");

        var content = ReadString("Введите содержание иска");

        var claim = courtCase.CreateClaim(
            new ClaimContent(content)
        );

        PrintSuccess("Иск создан.");
        PrintClaim(claim);
    }

    private static void AssignArbitrator()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("Назначение арбитра");

        var arbitrator = SelectArbitrator();

        var result = courtCase.AssignArbitrator(arbitrator);

        if (result)
        {
            PrintSuccess("Арбитр назначен.");
        }

        PrintCase(courtCase, Cases.IndexOf(courtCase));
    }

    private static void AddComment()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("Добавление комментария");
        Console.WriteLine("1. Истец");
        Console.WriteLine("2. Ответчик");
        Console.WriteLine("3. Арбитр");

        var authorType = ReadString("Кто оставляет комментарий");
        var content = new CommentContent(ReadString("Введите текст комментария"));

        Comment comment;

        switch (authorType)
        {
            case "1":
                {
                    var plaintiff = SelectPlaintiff();
                    comment = courtCase.AddCommentByPlaintiff(plaintiff, content);
                    break;
                }

            case "2":
                {
                    var defendant = SelectDefendant();
                    comment = courtCase.AddCommentByDefendant(defendant, content);
                    break;
                }

            case "3":
                {
                    var arbitrator = SelectArbitrator();
                    comment = courtCase.AddCommentByArbitrator(arbitrator, content);
                    break;
                }

            default:
                throw new InvalidOperationException("Неизвестный тип автора комментария.");
        }

        PrintSuccess("Комментарий добавлен.");
        PrintComment(comment, courtCase.Comments.Count - 1);
    }

    private static void CreateProposal()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("Создание предложения об урегулировании");

        var defendant = SelectDefendant();
        var content = ReadString("Введите содержание предложения");

        var proposal = courtCase.CreateProposal(
            defendant,
            new ProposalContent(content)
        );

        PrintSuccess("Предложение создано.");
        PrintProposal(proposal, courtCase.Proposals.Count - 1);
    }

    private static void AcceptProposal()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("Принятие предложения");

        var plaintiff = SelectPlaintiff();
        var proposal = SelectProposalFromCurrentCase(courtCase);

        var result = courtCase.AcceptProposal(plaintiff, proposal);

        if (result)
        {
            PrintSuccess("Предложение принято.");
        }

        PrintProposal(proposal, GetProposalIndex(courtCase, proposal));
        PrintCase(courtCase, Cases.IndexOf(courtCase));
    }

    private static void RejectProposal()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("Отклонение предложения");

        var plaintiff = SelectPlaintiff();
        var proposal = SelectProposalFromCurrentCase(courtCase);

        var result = courtCase.RejectProposal(plaintiff, proposal);

        if (result)
        {
            PrintSuccess("Предложение отклонено.");
        }

        PrintProposal(proposal, GetProposalIndex(courtCase, proposal));
        PrintCase(courtCase, Cases.IndexOf(courtCase));
    }

    private static void IssueVerdict()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("Вынесение вердикта");

        var arbitrator = SelectArbitrator();
        var content = ReadString("Введите содержание вердикта");

        var verdict = courtCase.IssueVerdict(
            arbitrator,
            new VerdictContent(content)
        );

        PrintSuccess("Вердикт вынесен.");
        PrintVerdict(verdict);
        PrintCase(courtCase, Cases.IndexOf(courtCase));
    }

    // ============================================================
    // CHANGE
    // ============================================================

    private static void ChangePlaintiffName()
    {
        Console.WriteLine("Изменение имени истца");

        var plaintiff = SelectPlaintiff();

        var firstName = ReadString("Введите новое имя истца");
        var lastName = ReadString("Введите новую фамилию истца");

        plaintiff.ChangeName(
            new FirstName(firstName)
        );

        PrintSuccess("Имя истца изменено.");
        PrintPlaintiff(plaintiff, Plaintiffs.IndexOf(plaintiff));
    }

    private static void ChangeDefendantName()
    {
        Console.WriteLine("Изменение имени ответчика");

        var defendant = SelectDefendant();

        var firstName = ReadString("Введите новое имя ответчика");
        var lastName = ReadString("Введите новую фамилию ответчика");

        defendant.ChangeName(
            new FirstName(firstName)
        );

        PrintSuccess("Имя ответчика изменено.");
        PrintDefendant(defendant, Defendants.IndexOf(defendant));
    }

    private static void ChangeArbitratorData()
    {
        Console.WriteLine("Изменение данных арбитра");

        var arbitrator = SelectArbitrator();

        Console.WriteLine("1. Изменить имя");
        Console.WriteLine("2. Изменить биографию");

        var command = ReadString("Выберите действие");

        switch (command)
        {
            case "1":
                {
                    var firstName = ReadString("Введите новое имя арбитра");
                    var lastName = ReadString("Введите новую фамилию арбитра");

                    arbitrator.ChangeName(
                        new FirstName(firstName)
                    );

                    PrintSuccess("Имя арбитра изменено.");
                    break;
                }

            case "2":
                {
                    var bio = ReadString("Введите новую биографию арбитра");

                    arbitrator.ChangeBio(bio);

                    PrintSuccess("Биография арбитра изменена.");
                    break;
                }

            default:
                throw new InvalidOperationException("Неизвестная команда.");
        }

        PrintArbitrator(arbitrator, Arbitrators.IndexOf(arbitrator));
    }

    // ============================================================
    // SHOW
    // ============================================================

    private static void SelectCurrentCase()
    {
        var courtCase = SelectCase();
        CurrentCase = courtCase;

        PrintSuccess("Текущее дело выбрано.");
        PrintCase(courtCase, Cases.IndexOf(courtCase));
    }

    private static void ShowCurrentCaseDetails()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("==================================================");
        Console.WriteLine("                ПОДРОБНОСТИ ДЕЛА");
        Console.WriteLine("==================================================");

        Console.WriteLine();
        Console.WriteLine("------------------ ПРАВИЛА ДЕЛА ------------------");

        if (courtCase.Rules.Count == 0)
        {
            Console.WriteLine("Правила к делу не добавлены.");
        }
        else
        {
            var rules = courtCase.Rules.ToList();

            for (var i = 0; i < rules.Count; i++)
            {
                PrintCourtRule(rules[i], i);
            }
        }

        PrintCase(courtCase, Cases.IndexOf(courtCase));

        Console.WriteLine();
        Console.WriteLine("-------------------- ИСК --------------------");

        if (courtCase.Claim is null)
        {
            Console.WriteLine("Иск не создан.");
        }
        else
        {
            PrintClaim(courtCase.Claim);
        }

        Console.WriteLine();
        Console.WriteLine("---------------- КОММЕНТАРИИ ----------------");

        if (courtCase.Comments.Count == 0)
        {
            Console.WriteLine("Комментариев нет.");
        }
        else
        {
            var comments = courtCase.Comments.ToList();

            for (var i = 0; i < comments.Count; i++)
            {
                PrintComment(comments[i], i);
            }
        }

        Console.WriteLine();
        Console.WriteLine("--------------- ПРЕДЛОЖЕНИЯ ----------------");

        if (courtCase.Proposals.Count == 0)
        {
            Console.WriteLine("Предложений нет.");
        }
        else
        {
            var proposals = courtCase.Proposals.ToList();

            for (var i = 0; i < proposals.Count; i++)
            {
                PrintProposal(proposals[i], i);
            }
        }

        Console.WriteLine();
        Console.WriteLine("------------------ ВЕРДИКТ ------------------");

        if (courtCase.Verdict is null)
        {
            Console.WriteLine("Вердикт не вынесен.");
        }
        else
        {
            PrintVerdict(courtCase.Verdict);
        }
    }

    private static void ShowAllObjects()
    {
        Console.WriteLine("==================================================");
        Console.WriteLine("                    ИСТЦЫ");
        Console.WriteLine("==================================================");

        if (Plaintiffs.Count == 0)
        {
            Console.WriteLine("Истцов нет.");
        }
        else
        {
            for (var i = 0; i < Plaintiffs.Count; i++)
            {
                PrintPlaintiff(Plaintiffs[i], i);
            }
        }

        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("                  ОТВЕТЧИКИ");
        Console.WriteLine("==================================================");

        if (Defendants.Count == 0)
        {
            Console.WriteLine("Ответчиков нет.");
        }
        else
        {
            for (var i = 0; i < Defendants.Count; i++)
            {
                PrintDefendant(Defendants[i], i);
            }
        }

        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("                   АРБИТРЫ");
        Console.WriteLine("==================================================");

        if (Arbitrators.Count == 0)
        {
            Console.WriteLine("Арбитров нет.");
        }
        else
        {
            for (var i = 0; i < Arbitrators.Count; i++)
            {
                PrintArbitrator(Arbitrators[i], i);
            }
        }

        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("                    ДЕЛА");
        Console.WriteLine("==================================================");

        if (Cases.Count == 0)
        {
            Console.WriteLine("Дел нет.");
        }
        else
        {
            for (var i = 0; i < Cases.Count; i++)
            {
                PrintCase(Cases[i], i);
            }
        }
    }

    // ============================================================
    // SELECT
    // ============================================================

    private static Plaintiff SelectPlaintiff()
    {
        if (Plaintiffs.Count == 0)
        {
            throw new InvalidOperationException("Сначала создайте хотя бы одного истца.");
        }

        Console.WriteLine("Доступные истцы:");

        for (var i = 0; i < Plaintiffs.Count; i++)
        {
            PrintPlaintiff(Plaintiffs[i], i);
        }

        var index = ReadIndex("Введите номер истца", Plaintiffs.Count);
        return Plaintiffs[index];
    }

    private static Defendant SelectDefendant()
    {
        if (Defendants.Count == 0)
        {
            throw new InvalidOperationException("Сначала создайте хотя бы одного ответчика.");
        }

        Console.WriteLine("Доступные ответчики:");

        for (var i = 0; i < Defendants.Count; i++)
        {
            PrintDefendant(Defendants[i], i);
        }

        var index = ReadIndex("Введите номер ответчика", Defendants.Count);
        return Defendants[index];
    }

    private static Arbitrator SelectArbitrator()
    {
        if (Arbitrators.Count == 0)
        {
            throw new InvalidOperationException("Сначала создайте хотя бы одного арбитра.");
        }

        Console.WriteLine("Доступные арбитры:");

        for (var i = 0; i < Arbitrators.Count; i++)
        {
            PrintArbitrator(Arbitrators[i], i);
        }

        var index = ReadIndex("Введите номер арбитра", Arbitrators.Count);
        return Arbitrators[index];
    }

    private static Case SelectCase()
    {
        if (Cases.Count == 0)
        {
            throw new InvalidOperationException("Сначала создайте хотя бы одно дело.");
        }

        Console.WriteLine("Доступные дела:");

        for (var i = 0; i < Cases.Count; i++)
        {
            PrintCase(Cases[i], i);
        }

        var index = ReadIndex("Введите номер дела", Cases.Count);
        return Cases[index];
    }

    private static SettlementProposal SelectProposalFromCurrentCase(Case courtCase)
    {
        if (courtCase.Proposals.Count == 0)
        {
            throw new InvalidOperationException("У текущего дела нет предложений.");
        }

        var proposals = courtCase.Proposals.ToList();

        Console.WriteLine("Предложения текущего дела:");

        for (var i = 0; i < proposals.Count; i++)
        {
            PrintProposal(proposals[i], i);
        }

        var index = ReadIndex("Введите номер предложения", proposals.Count);
        return proposals[index];
    }

    // ============================================================
    // RULES
    // ============================================================
    private static void CreateCourtRule()
    {
        Console.WriteLine("Создание правила суда");

        var title = ReadString("Введите название правила");
        var description = ReadString("Введите описание правила");

        var rule = new CourtRule(
            new RuleTitle(title),
            new RuleContent(description)
        );

        CourtRules.Add(rule);

        PrintSuccess("Правило суда создано.");
        PrintCourtRule(rule, CourtRules.Count - 1);
    }

    private static void ShowAllCourtRules()
    {
        Console.WriteLine("============== ПРАВИЛА СУДА ==============");

        if (CourtRules.Count == 0)
        {
            Console.WriteLine("Правил пока нет.");
            return;
        }

        for (var i = 0; i < CourtRules.Count; i++)
        {
            PrintCourtRule(CourtRules[i], i);
        }
    }

    private static void AddRuleToCurrentCase()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("Добавление правила к текущему делу");

        var rule = SelectCourtRule();

        courtCase.AddRule(rule);

        PrintSuccess("Правило добавлено к делу.");
        PrintCourtRule(rule, CourtRules.IndexOf(rule));
    }

    private static void RemoveRuleFromCurrentCase()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("Удаление правила из текущего дела");

        var rule = SelectRuleFromCurrentCase(courtCase);

        courtCase.RemoveRule(rule);

        PrintSuccess("Правило удалено из дела.");
    }

    private static void ShowCurrentCaseRules()
    {
        var courtCase = RequireCurrentCase();

        Console.WriteLine("============== ПРАВИЛА ТЕКУЩЕГО ДЕЛА ==============");

        if (courtCase.Rules.Count == 0)
        {
            Console.WriteLine("К текущему делу правила не добавлены.");
            return;
        }

        var rules = courtCase.Rules.ToList();

        for (var i = 0; i < rules.Count; i++)
        {
            PrintCourtRule(rules[i], i);
        }
    }

    private static CourtRule SelectCourtRule()
    {
        if (CourtRules.Count == 0)
        {
            throw new InvalidOperationException("Сначала создайте хотя бы одно правило суда.");
        }

        Console.WriteLine("Доступные правила суда:");

        for (var i = 0; i < CourtRules.Count; i++)
        {
            PrintCourtRule(CourtRules[i], i);
        }

        var index = ReadIndex("Введите номер правила", CourtRules.Count);

        return CourtRules[index];
    }

    private static CourtRule SelectRuleFromCurrentCase(Case courtCase)
    {
        if (courtCase.Rules.Count == 0)
        {
            throw new InvalidOperationException("У текущего дела нет правил.");
        }

        var rules = courtCase.Rules.ToList();

        Console.WriteLine("Правила текущего дела:");

        for (var i = 0; i < rules.Count; i++)
        {
            PrintCourtRule(rules[i], i);
        }

        var index = ReadIndex("Введите номер правила", rules.Count);

        return rules[index];
    }

    private static void PrintCourtRule(CourtRule rule, int index)
    {
        Console.WriteLine($"[{index}] CourtRule");
        Console.WriteLine($"     Id: {rule.Id}");
        Console.WriteLine($"     Название: {rule.Title}");
        Console.WriteLine($"     Описание: {rule.Description}");
    }

    // ============================================================
    // PRINT
    // ============================================================

    private static void PrintPlaintiff(Plaintiff plaintiff, int index)
    {
        Console.WriteLine($"[{index}] Plaintiff");
        Console.WriteLine($"     Id: {plaintiff.Id}");
        Console.WriteLine($"     Имя: {plaintiff.Name}");
        Console.WriteLine($"     Фамилия: {plaintiff.Surname}");
    }

    private static void PrintDefendant(Defendant defendant, int index)
    {
        Console.WriteLine($"[{index}] Defendant");
        Console.WriteLine($"     Id: {defendant.Id}");
        Console.WriteLine($"     Имя: {defendant.Name}");
        Console.WriteLine($"     Фамилия: {defendant.Surname}");
    }

    private static void PrintArbitrator(Arbitrator arbitrator, int index)
    {
        Console.WriteLine($"[{index}] Arbitrator");
        Console.WriteLine($"     Id: {arbitrator.Id}");
        Console.WriteLine($"     Имя: {arbitrator.Name}");
        Console.WriteLine($"     Фамилия: {arbitrator.Surname}");
        Console.WriteLine($"     Bio: {arbitrator.Bio}");
        Console.WriteLine($"     Опыт: {arbitrator.Expirience}");
        Console.WriteLine($"     Активен: {arbitrator.IsActived}");
    }

    private static void PrintCase(Case courtCase, int index)
    {
        Console.WriteLine($"[{index}] Case");
        Console.WriteLine($"     Id: {courtCase.Id}");
        Console.WriteLine($"     Название: {courtCase.Title}");
        Console.WriteLine($"     Описание: {courtCase.Description}");
        Console.WriteLine($"     Истец: {courtCase.Plaintiff.Name} {courtCase.Plaintiff.Surname}");
        Console.WriteLine($"     Ответчик: {courtCase.Defendant.Name} {courtCase.Defendant.Surname}");

        if (courtCase.Arbitrator is null)
        {
            Console.WriteLine("     Арбитр: не назначен");
        }
        else
        {
            Console.WriteLine($"     Арбитр: {courtCase.Arbitrator.Name} {courtCase.Arbitrator.Surname}");
        }

        Console.WriteLine($"     Статус: {courtCase.Status}");
        Console.WriteLine($"     Активно: {courtCase.isActive()}");
        Console.WriteLine($"     Иск создан: {(courtCase.Claim is null ? "нет" : "да")}");
        Console.WriteLine($"     Предложений: {courtCase.Proposals.Count}");
        Console.WriteLine($"     Комментариев: {courtCase.Comments.Count}");
        Console.WriteLine($"     Вердикт вынесен: {(courtCase.Verdict is null ? "нет" : "да")}");
        Console.WriteLine($"     Закрыто: {(courtCase.ClosedAt is null ? "нет" : courtCase.ClosedAt)}");
    }

    private static void PrintClaim(Claim claim)
    {
        Console.WriteLine("Claim");
        Console.WriteLine($"     Истец: {claim.Plaintiff.Name} {claim.Plaintiff.Surname}");
        Console.WriteLine($"     Ответчик: {claim.Defendant.Name} {claim.Defendant.Surname}");
        Console.WriteLine($"     Содержание: {claim.Content}");
    }

    private static void PrintComment(Comment comment, int index)
    {
        Console.WriteLine($"[{index}] Comment");
        Console.WriteLine($"     Id: {comment.Id}");
        Console.WriteLine($"     CaseId: {comment.CaseId}");
        Console.WriteLine($"     AuthorId: {comment.AuthorId}");
        Console.WriteLine($"     Содержание: {comment.Content}");
    }

    private static void PrintProposal(SettlementProposal proposal, int index)
    {
        Console.WriteLine($"[{index}] SettlementProposal");
        Console.WriteLine($"     Id: {proposal.Id}");
        Console.WriteLine($"     CaseId: {proposal.CaseId}");
        Console.WriteLine($"     Ответчик: {proposal.Defendant.Name} {proposal.Defendant.Surname}");
        Console.WriteLine($"     Статус: {proposal.Status}");
        Console.WriteLine($"     Содержание: {proposal.Content}");
    }

    private static void PrintVerdict(Verdict verdict)
    {
        Console.WriteLine("Verdict");
        Console.WriteLine($"     Id: {verdict.Id}");
        Console.WriteLine($"     CaseId: {verdict.CaseId}");
        Console.WriteLine($"     Арбитр: {verdict.Arbitrator.Name} {verdict.Arbitrator.Surname}");
        Console.WriteLine($"     Содержание: {verdict.Content}");
    }

    // ============================================================
    // HELPERS
    // ============================================================

    private static Case RequireCurrentCase()
    {
        if (CurrentCase is null)
        {
            throw new InvalidOperationException("Текущее дело не выбрано. Создайте дело или выберите его через пункт 5.");
        }

        return CurrentCase;
    }

    private static int GetProposalIndex(Case courtCase, SettlementProposal proposal)
    {
        return courtCase.Proposals.ToList().IndexOf(proposal);
    }

    private static string ReadString(string message)
    {
        while (true)
        {
            Console.Write($"{message}: ");
            var value = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }

            PrintWarning("Значение не может быть пустым.");
        }
    }

    private static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write($"{message}: ");
            var value = Console.ReadLine();

            if (int.TryParse(value, out var result))
            {
                return result;
            }

            PrintWarning("Введите корректное число.");
        }
    }

    private static int ReadIndex(string message, int count)
    {
        while (true)
        {
            var index = ReadInt(message);

            if (index >= 0 && index < count)
            {
                return index;
            }

            PrintWarning($"Введите число от 0 до {count - 1}.");
        }
    }

    private static void PrintSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void PrintWarning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    private static void PrintError(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Ошибка:");
        Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
        Console.ResetColor();
    }

    private static void Pause()
    {
        Console.WriteLine();
        Console.WriteLine("Нажмите Enter, чтобы продолжить...");
        Console.ReadLine();
    }


    // =======================================================
    // SEEDDATA
    // =======================================================
    private static void SeedData()
    {
        // Чтобы при случайном повторном вызове данные не дублировались
        if (Plaintiffs.Count > 0 ||
            Defendants.Count > 0 ||
            Arbitrators.Count > 0 ||
            Cases.Count > 0 ||
            CourtRules.Count > 0)
        {
            return;
        }

        // =========================
        // Истцы
        // =========================

        var plaintiff1 = new Plaintiff(
            new FirstName("Иван"),
            new LastName("Крыса")
        );

        var plaintiff2 = new Plaintiff(
            new FirstName("Алексей"),
            new LastName("Плаксикович")
        );

        Plaintiffs.Add(plaintiff1);
        Plaintiffs.Add(plaintiff2);

        // =========================
        // Ответчики
        // =========================

        var defendant1 = new Defendant(
            new FirstName("Петр"),
            new LastName("Газмясов")
        );

        var defendant2 = new Defendant(
            new FirstName("Сергей"),
            new LastName("ОтвечаюНеВиновен")
        );

        Defendants.Add(defendant1);
        Defendants.Add(defendant2);

        // =========================
        // Арбитры
        // =========================

        var arbitrator1 = new Arbitrator(
            new FirstName("Анна"),
            new LastName("Правосудьева"),
            "Арбитр по коммерческим и договорным спорам.",
            10
        );

        var arbitrator2 = new Arbitrator(
            new FirstName("Мария"),
            new LastName("ПапаПрокуроровна"),
            "Арбитр с опытом рассмотрения споров о поставке и оказании услуг.",
            7
        );

        Arbitrators.Add(arbitrator1);
        Arbitrators.Add(arbitrator2);

        // =========================
        // Правила суда
        // =========================

        var rule1 = new CourtRule(
            new RuleTitle("Actuallyism"),
            new RuleContent("Все возражения принимаются только в форме “ну это вообще-то…” с поднятым указательным пальцем.")
        );

        var rule2 = new CourtRule(
            new RuleTitle("Gavelrage"),
            new RuleContent("Судья имеет право стукнуть молотком только после фразы: “Так, всё, я сейчас разберусь как взрослый человек”.")
        );

        var rule3 = new CourtRule(
            new RuleTitle("Memesummary"),
            new RuleContent("Сторона, затянувшая речь дольше трёх минут, обязана кратко пересказать её мемом.")
        );

        var rule4 = new CourtRule(
        new RuleTitle("Topsecretfolder"),
        new RuleContent("Доказательства принимаются только в папке с надписью “Суперважное, не открывать”.")
        );

        var rule5 = new CourtRule(
        new RuleTitle("Nonlawyerism"),
        new RuleContent("Каждый участник процесса должен хотя бы один раз сказать: “Я не юрист, но звучит убедительно”.")
        );

        var rule6 = new CourtRule(
        new RuleTitle("Precedenting"),
        new RuleContent("При слове “прецедент” все делают серьёзное лицо и кивают, даже если никто не понял, о чём речь.")
        );

        var rule7 = new CourtRule(
        new RuleTitle("Bureaucracywin"),
        new RuleContent("Проигравшая сторона обязана торжественно признать: “Ладно, в этот раз бюрократия победила”.")
        );

        CourtRules.Add(rule1);
        CourtRules.Add(rule2);
        CourtRules.Add(rule3);
        CourtRules.Add(rule4);
        CourtRules.Add(rule5);
        CourtRules.Add(rule6);
        CourtRules.Add(rule7);

        // =========================
        // Дело 1
        // =========================

        var case1 = new Case(
            plaintiff1,
            defendant1,
            new CaseTitle("Спор о нарушении договора поставки"),
            new CaseDescription("Истец утверждает, что ответчик нарушил сроки поставки товара и не выплатил неустойку.")
        );

        case1.AddRule(rule1);
        case1.AddRule(rule3);

        Cases.Add(case1);

        // =========================
        // Дело 2
        // =========================

        var case2 = new Case(
            plaintiff2,
            defendant2,
            new CaseTitle("Спор о задолженности по договору услуг"),
            new CaseDescription("Истец требует взыскать задолженность за оказанные услуги.")
        );

        case2.AddRule(rule1);
        case2.AddRule(rule2);

        Cases.Add(case2);

        CurrentCase = case1;
    }
}