namespace AC.Domain.Entities.Exceptions;

public class ArbitratorAlreadyAssignedException(Arbitrator arbitrator, Case casename)
    : InvalidOperationException($"Arbitrator {arbitrator.Name} cannot be assigned to case {casename.Title}. Case already has an arbitrator assigned.")
{
    public Arbitrator _abitrator => arbitrator;
    public Case _case => casename;
}
