using AC.Domain.ValueObjects;


namespace AC.Domain.Entities;

public class Plaintiff : Entity
{
    // ПОЛЯ
    public FullName Name { get; private set; }

    // МЕТОДЫ
    public void ChangeName(FullName newName)
    {
        if (newName == null)
            throw new ArgumentNullException(nameof(newName));

        Name = newName;
    }

    // КОНСТРУКТОРЫ
    public Plaintiff(FullName name) : base()
    {
        Name = name;
    }
}