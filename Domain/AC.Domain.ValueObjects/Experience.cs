using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Validators;

namespace AC.Domain.ValueObjects;

public class Experience(int content) : ValueObject<int>(new ExperienceValidator(), content);