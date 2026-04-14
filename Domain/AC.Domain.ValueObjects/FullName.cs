using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class FullName(string name) : ValueObject<string>(new FullNameValidator(), name);