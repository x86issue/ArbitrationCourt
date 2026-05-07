using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects.archive;

public class FullName(string name) : ValueObject<string>(new FullNameValidator(), name);