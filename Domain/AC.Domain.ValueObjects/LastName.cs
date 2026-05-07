using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class LastName(string surname) : ValueObject<string>(new LastNameValidator(), surname);