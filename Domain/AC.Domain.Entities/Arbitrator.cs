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
    public void ChangeBio(string newBio)
    {
        if (string.IsNullOrEmpty(newBio))
            throw new ArgumentNullException(nameof(newBio));

        Bio = newBio;
    }


    public void ChangeName(FullName newName)
    {
        if (newName == null)
            throw new ArgumentNullException(nameof(newName));

        Name = newName;
    }

    // КОНТРУКТОРЫ
    public Arbitrator() { }

    public Arbitrator(FullName name, string? bio, int expirience)
    {
        Name = name;
        Bio = bio;
        Expirience = expirience;
    }
}

