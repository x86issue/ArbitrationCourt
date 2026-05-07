using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Exceptions;


namespace AC.Domain.ValueObjects.Validators;

public class RuleTitleValidator : IValidator<string>
{
    public void Validate(string value)
    {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Название правила не может быть пустым.", nameof(value));

            if (value.Length < 2)
                throw new ArgumentException("Название правила должно содержать минимум 2 символа.", nameof(value));

            if (value.Length > 100)
                throw new ArgumentException("Название правила не может быть длиннее 100 символов.", nameof(value));
        }

}