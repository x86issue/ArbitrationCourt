using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class Biography(string content) : ValueObject<string>(new BioValidator(), content);