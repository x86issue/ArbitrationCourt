using AC.Domain.ValueObjects;
using AC.Domain.ValueObjects.Validators;


namespace AC.Domain.Entities;

public class Arbitrator : Entity
{
    // ПОЛЯ
    public FirstName Name { get; private set; }
    public LastName Surname { get; private set; }

    public string? Bio { get; private set; }
    public int Expirience { get; private set; }
    public bool IsActived { get; } = true;


    // МЕТОДЫ
    public void ChangeBio(string newBio)
    {
        if (string.IsNullOrEmpty(newBio))
            throw new ArgumentNullException(nameof(newBio));

        Bio = newBio;
    }


    public bool ChangeName(FirstName newName)
    {
        if (newName == null) throw new ArgumentNullException(nameof(newName));

        Name = newName;
        return true;
    }


    // КОНТРУКТОРЫ
    protected Arbitrator() { } // поч сломан разобраться

    public Arbitrator(FirstName name,LastName surname, string? bio, int expirience)
    {
        Name = name;
        Surname = surname;
        Bio = bio;
        Expirience = expirience;
    }
}

