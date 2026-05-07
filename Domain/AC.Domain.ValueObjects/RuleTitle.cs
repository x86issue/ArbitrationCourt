using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

public class RuleTitle(string title) : ValueObject<string>(new RuleTitleValidator(), title);