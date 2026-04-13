using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class CommentContent(string content) : ValueObject<string>(new CommentContentValidator(), content);