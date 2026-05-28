using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class RuleTitle(string title) : ValueObject<string>(new RuleTitleValidator(), title);