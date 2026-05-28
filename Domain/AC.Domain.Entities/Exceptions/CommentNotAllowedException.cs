namespace AC.Domain.Entities.Exceptions;

public class CommentNotAllowedException()
    : InvalidOperationException($"Comment is not allowed to be added to the case.")
{
}
