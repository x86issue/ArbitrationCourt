namespace AC.Domain.Entities.Exceptions;

public class VerdictAlreadyExistsException(Verdict verdict)
    : InvalidOperationException($"A verdict with the same case id {verdict.CaseId} already exists.");