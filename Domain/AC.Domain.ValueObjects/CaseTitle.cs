using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class CaseTitle(string title) : ValueObject<string>(new CaseTitleValidator(), title);