using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Exceptions;

namespace AC.Domain.ValueObjects.archive;

public class FullNameValidator : IValidator<string>
{
    public static int MAX_FIRST_NAME_LENGTH => 50;
    public static int MIN_FIRST_NAME_LENGTH => 2;

    public static int MAX_LAST_NAME_LENGTH => 50;
    public static int MIN_LAST_NAME_LENGTH => 2;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        // ПРОХАВАТЬ ЧЕ НАПИСАНО ПАРТ 2 (АВТОМАТИЧЕСКИ ВСТАВИЛ VS)
        var parts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
            throw new FormatException($"The \"{nameof(value)}\" must consist of first name and last name separated by a space.");
        var firstName = parts[0];
        var lastName = parts[1];
        if (firstName.Length > MAX_FIRST_NAME_LENGTH)
            throw new ArgumentLongValueException(nameof(firstName), firstName, MAX_FIRST_NAME_LENGTH);
        if (firstName.Length < MIN_FIRST_NAME_LENGTH)
            throw new ArgumentShortValueException(nameof(firstName), firstName, MIN_FIRST_NAME_LENGTH);
        if (lastName.Length > MAX_LAST_NAME_LENGTH)
            throw new ArgumentLongValueException(nameof(lastName), lastName, MAX_LAST_NAME_LENGTH);
        if (lastName.Length < MIN_LAST_NAME_LENGTH)
            throw new ArgumentShortValueException(nameof(lastName), lastName, MIN_LAST_NAME_LENGTH);
    }

}


