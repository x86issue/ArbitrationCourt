using AC.Domain.Entities.Enums;
using AC.Domain.ValueObjects;


namespace AC.Domain.Entities;

public class Arbitrator : Entity
{
    // ПОЛЯ
    public FullName Name { get; private set; }

    public string? Bio { get; private set; }
    public int Expirience { get; private set; }
    public bool IsActived { get; } = true;


    // МЕТОДЫ


    // КОНТРУКТОРЫ
    public Arbitrator(FullName name, string? bio, int expirience)
    {
        Name = name;
        Bio = bio;
        Expirience = expirience;
    }
}

