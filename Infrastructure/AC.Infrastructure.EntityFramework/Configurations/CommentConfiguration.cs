using AC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.EntityFramework.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        builder.ConfigureEntity();
        builder.Property(comment => comment.Content).HasCommentContentConversion().HasMaxLength(1000).IsRequired();
        builder.Property(comment => comment.AuthorId).IsRequired();
        builder.Property<Guid>("CaseId").IsRequired();
        builder.HasIndex("CaseId");
        builder.Ignore(comment => comment.AuthorPlaintiff);
        builder.Ignore(comment => comment.AuthorDefendant);
        builder.Ignore(comment => comment.AuthorArbitrator);
    }
}
