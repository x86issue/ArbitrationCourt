using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class CourtRule : Entity<Guid>
{
    public RuleTitle Title { get; private set; } = null!;
    public RuleContent Description { get; private set; } = null!;

    protected CourtRule()
    {
    }

    public bool ChangeTitle(RuleTitle title)
    {
        if (Title != title)
        {
            Title = title;
            return true;
        }

        return false;
    }

    public bool ChangeDescription(RuleContent description)
    {
        if (Description != description)
        {
            Description = description;
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return $"{Title}: {Description}";
    }

    public CourtRule(DateTime created, RuleTitle title, RuleContent description)
        : this(Guid.NewGuid(), created, title, description)
    {
    }

    protected CourtRule(Guid id, DateTime created, RuleTitle title, RuleContent description) : base(id)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
}
