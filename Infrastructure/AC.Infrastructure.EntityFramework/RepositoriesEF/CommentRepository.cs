using AC.Domain.Entities;
using AC.Domain.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AC.Infrastructure.EntityFramework.RepositoriesEF;

public class CommentRepository(ApplicationDbContext dbContext) : EFRepository<Comment, Guid>(dbContext), ICommentRepository
{
    public async Task<IReadOnlyList<Comment>> GetByCaseIdAsync(Guid caseId, CancellationToken cancellationToken = default)
    {
        return await DbContext.Comments
            .AsNoTracking()
            .Where(comment => EF.Property<Guid>(comment, "CaseId") == caseId)
            .ToListAsync(cancellationToken);
    }
}
