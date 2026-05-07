using AC.Domain.ValueObjects;


namespace AC.Domain.Entities;

public class Defendant : Entity
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
    protected Defendant() { }

    public Defendant(FirstName name, LastName surname) : base()
    {
        Name = name;
        Surname = surname;
    }
}