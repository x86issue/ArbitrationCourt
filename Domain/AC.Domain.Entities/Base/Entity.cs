namespace AC.Domain.Entities;

public abstract class Entity
{

    // поля ид и дата создания
    public Guid Id { get; }
    public DateTime CreatedAt { get; }

    // контруктор по умолчанию
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
