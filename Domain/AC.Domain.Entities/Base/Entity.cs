using System.Security.Cryptography;

namespace AC.Domain.Entities;

//public abstract class Entity
//{

//    // поля ид и дата создания
//    public Guid Id { get; }
//    public DateTime CreatedAt { get; }

//    // контруктор по умолчанию
//    protected Entity()
//    {
//        Id = Guid.NewGuid();
//        CreatedAt = DateTime.UtcNow;
//    }
//}



public abstract class Entity
{

    public Guid Id { get; }
    public DateTime CreatedAt { get; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    // Защищённый конструктор для EF
    //protected Entity() : this(default) { }

}





//public abstract class Entity<TId> where TId : struct, IEquatable<TId>
//{
//    protected Entity(TId id)
//    {
//        Id = id;
//        CreatedAt = DateTime.UtcNow;
//    }

//    public TId Id { get; }
//    public DateTime CreatedAt { get; }

//    // Защищённый конструктор для EF
//    protected Entity() : this(default) { }
//}