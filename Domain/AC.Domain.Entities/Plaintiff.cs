using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class Plaintiff : Entity<Guid>
{
    public FirstName Name { get; private set; } = null!;
    public LastName Surname { get; private set; } = null!;

    protected Plaintiff()
    {
    }

    public bool ChangeName(FirstName newName)
    {
        Name = newName ?? throw new ArgumentNullException(nameof(newName));
        return true;
    }

    public Plaintiff(DateTime created, FirstName name, LastName surname)
        : this(Guid.NewGuid(), created, name, surname)
    {
    }

    protected Plaintiff(Guid id, DateTime created, FirstName name, LastName surname) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Surname = surname ?? throw new ArgumentNullException(nameof(surname));
    }
}
