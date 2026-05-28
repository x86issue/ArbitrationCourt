namespace AC.Domain.Entities.Exceptions;


public class InvalidCaseStateException(Case casename)
    : InvalidOperationException($"The case with id {casename.Id} is in an invalid state for this operation.")
{
    public Case _case => casename;
}
