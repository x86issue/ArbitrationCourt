using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class Defendant : Entity<Guid>
{
    public FirstName Name { get; private set; } = null!;
    public LastName Surname { get; private set; } = null!;

    protected Defendant()
    {
    }

    public bool ChangeName(FirstName newName)
    {
        Name = newName ?? throw new ArgumentNullException(nameof(newName));
        return true;
    }

    public Defendant(DateTime created, FirstName name, LastName surname)
        : this(Guid.NewGuid(), created, name, surname)
    {
    }

    protected Defendant(Guid id, DateTime created, FirstName name, LastName surname) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Surname = surname ?? throw new ArgumentNullException(nameof(surname));
    }
}
