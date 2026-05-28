using AC.Domain.ValueObjects.Base;
using AC.Domain.ValueObjects.Exceptions;

namespace AC.Domain.ValueObjects.Validators;

public class ExperienceValidator : IValidator<int>
{
	public static int MIN_EXPERIENCE_TIME = 0;

	public void Validate(int value)
	{
		if (value < MIN_EXPERIENCE_TIME) throw new ArgumentOutOfRangeException("value");
	}
}