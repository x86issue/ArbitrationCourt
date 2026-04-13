using AC.Domain.Entities.Enums;
using AC.Domain.ValueObjects;


namespace AC.Domain.Entities.Entities;

public class Plaintiff : Entity
{
    // ПОЛЯ
    public FullName Name { get; private set; }

    // МЕТОДЫ


    // КОНСТРУКТОРЫ
    public Plaintiff(FullName name) : base()
    {
        Name = name;
    }
}