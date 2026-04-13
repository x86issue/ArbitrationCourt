namespace AC.Domain.Entities.Exceptions;

public class CommentNotAllowedException(Comment comment)
    : InvalidOperationException($"Comment with id {comment.Id} is not allowed to be added to the case with id {comment.CaseId}.");
