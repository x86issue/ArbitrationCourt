using AC.Domain.ValueObjects;


namespace AC.Domain.Entities;

public class Plaintiff : Entity
{
    // ПОЛЯ
    public FirstName Name { get; private set; }
    public LastName Surname { get; private set; }

    // МЕТОДЫ
    public bool ChangeName(FirstName newName)
    {
        Name = newName;
        return true;
    }

    // КОНСТРУКТОРЫ
    protected Plaintiff() { }
    public Plaintiff(FirstName name, LastName surname) : base()
    {
        Name = name;
        Surname = surname;
    }
}