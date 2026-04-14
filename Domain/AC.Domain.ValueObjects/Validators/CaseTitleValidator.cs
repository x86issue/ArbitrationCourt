using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Exceptions;


namespace AC.Domain.ValueObjects.Validators;

public class CaseTitleValidator : IValidator<string>
{
    public static int MAX_LENGTH => 200;

    public static int MIN_LENGTH => 10;

    public void Validate(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));
        if(value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
        if(value.Length < MIN_LENGTH) 
            throw new ArgumentShortValueException(nameof(value), value, MIN_LENGTH);
    }
}