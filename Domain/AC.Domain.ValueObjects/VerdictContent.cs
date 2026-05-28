using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class VerdictContent(string content) : ValueObject<string>(new VerdictContentValidator(), content);