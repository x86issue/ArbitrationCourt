using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Exceptions;

namespace AC.Domain.ValueObjects.Validators;

public class FirstNameValidator : IValidator<string>
{
    public static int MAX_FIRST_NAME_LENGTH => 50;
    public static int MIN_FIRST_NAME_LENGTH => 2;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        // ПРОХАВАТЬ ЧЕ НАПИСАНО ПАРТ 2 (АВТОМАТИЧЕСКИ ВСТАВИЛ VS)
        if (value.Length > MAX_FIRST_NAME_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_FIRST_NAME_LENGTH);
        if (value.Length < MIN_FIRST_NAME_LENGTH)
            throw new ArgumentShortValueException(nameof(value), value, MIN_FIRST_NAME_LENGTH);
    }

}


