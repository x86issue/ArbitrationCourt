using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class RuleContent(string content) : ValueObject<string>(new RuleContentValidator(), content);