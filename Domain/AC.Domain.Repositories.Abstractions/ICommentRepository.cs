using AC.Domain.Entities;

namespace AC.Domain.Repositories.Abstractions;

public interface ICommentRepository : IRepository<Comment, Guid>
{
    Task<IReadOnlyList<Comment>> GetByCaseIdAsync(Guid caseId, CancellationToken cancellationToken = default);
}
