using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class Arbitrator : Entity<Guid>
{
    public FirstName Name { get; private set; } = null!;
    public LastName Surname { get; private set; } = null!;
    public Biography? Bio { get; private set; }
    public Experience? Exp { get; private set; }
    public bool IsActived { get; private set; } = true;

    protected Arbitrator()
    {
    }

    public bool ChangeBio(Biography newBio)
    {
        if (Bio != newBio)
        {
            Bio = newBio;
            return true;
        }

        return false;
    }

    public bool ChangeName(FirstName newName)
    {
        if (newName == null) throw new ArgumentNullException(nameof(newName));

        if (Name != newName)
        {
            Name = newName;
            return true;
        }

        return false;
    }

    public Arbitrator(DateTime created, FirstName name, LastName surname, Biography? bio, Experience? expirience)
        : this(Guid.NewGuid(), created, name, surname, bio, expirience)
    {
    }

    protected Arbitrator(Guid id, DateTime created, FirstName name, LastName surname, Biography? bio, Experience? exp) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Surname = surname ?? throw new ArgumentNullException(nameof(surname));
        Bio = bio;
        Exp = exp;
    }
}
