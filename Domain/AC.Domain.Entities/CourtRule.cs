using AC.Domain.ValueObjects;

namespace AC.Domain.Entities;

public class CourtRule : Entity
{
    public RuleTitle Title { get; private set; }
    public RuleContent Description { get; private set; }


    public bool ChangeTitle(RuleTitle title)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        return false;
    }

    public bool ChangeDescription(RuleContent description)
    {
        Description = description ?? throw new ArgumentNullException(nameof(description));
        return false;
    }

    public override string ToString()
    {
        return $"{Title}: {Description}";
    }

    protected CourtRule() { }

    public CourtRule(RuleTitle title, RuleContent description) : base()
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
}