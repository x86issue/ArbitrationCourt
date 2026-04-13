using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class CaseDescription(string description) : ValueObject<string>(new CaseDescriptionValidator(), description);