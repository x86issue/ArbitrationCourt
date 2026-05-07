using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class FirstName(string name) : ValueObject<string>(new FirstNameValidator(), name);