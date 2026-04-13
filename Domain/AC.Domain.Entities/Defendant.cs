using AC.Domain.Entities.Enums;
using AC.Domain.ValueObjects;


namespace AC.Domain.Entities;

public class Defendant : Entity
{
    // ПОЛЯ
    public FullName Name { get; private set; }

    // МЕТОДЫ




    // КОНСТРУКТОРЫ
    public Defendant(FullName name) : base()
    {
        Name = name;
    }
}