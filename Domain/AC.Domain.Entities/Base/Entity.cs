namespace AC.Domain.Entities;

public abstract class Entity<TId> where TId : struct, IEquatable<TId>
{
    protected Entity(TId id)
    {
        Id = id;
        CreatedAt = DateTime.Now;
    }

    protected Entity()
    {
    }

    public TId Id { get; private set; }
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
}
