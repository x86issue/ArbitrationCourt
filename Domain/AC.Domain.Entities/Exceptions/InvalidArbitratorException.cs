namespace AC.Domain.Entities.Exceptions;

public class InvalidArbitratorException(Arbitrator arbitrator)
    : InvalidOperationException($"The arbitrator with id {arbitrator.Id} is invalid.")
{
    public Arbitrator _arbitratotr => arbitrator;
}

