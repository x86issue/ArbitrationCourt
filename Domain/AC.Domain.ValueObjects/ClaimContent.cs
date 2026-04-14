using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class ClaimContent(string title) : ValueObject<string>(new ClaimContentValidator(), title);