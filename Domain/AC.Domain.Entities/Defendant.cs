using AC.Domain.ValueObjects;


namespace AC.Domain.Entities;

public class Defendant : Entity
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
    public Defendant(FullName name) : base()
    {
        Name = name;
    }
}