using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Exceptions;

namespace AC.Domain.ValueObjects.Validators;

public class CommentContentValidator : IValidator<string>
{
    public static int MAX_LENGTH => 1000;
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}

