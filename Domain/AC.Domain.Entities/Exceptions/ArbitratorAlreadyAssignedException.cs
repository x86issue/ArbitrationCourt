namespace AC.Domain.Entities.Exceptions;

public class ArbitratorAlreadyAssignedException(Arbitrator arbitrator, Case casename)
    : Exception($"Arbitrator {arbitrator.Name} cannot be assigned to case {casename.Title}. Case already has an arbitrator assigned.");