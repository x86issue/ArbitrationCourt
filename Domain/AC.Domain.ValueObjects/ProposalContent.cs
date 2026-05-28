using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class ProposalContent(string content) : ValueObject<string>(new ProposalContentValidator(), content);