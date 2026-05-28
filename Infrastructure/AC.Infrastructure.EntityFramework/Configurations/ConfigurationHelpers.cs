using AC.Domain.Entities;
using AC.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.EntityFramework.Configurations;

internal static class ConfigurationHelpers
{
    public static void ConfigureEntity<TEntity>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : Entity<Guid>
    {
        builder.HasKey(entity => entity.Id);
        builder.Property(entity => entity.Id).ValueGeneratedNever();
        builder.Property(entity => entity.CreatedAt).IsRequired();
    }

    public static PropertyBuilder<FirstName> HasFirstNameConversion(this PropertyBuilder<FirstName> builder)
        => builder.HasConversion(value => value.Value, value => new FirstName(value));

    public static PropertyBuilder<LastName> HasLastNameConversion(this PropertyBuilder<LastName> builder)
        => builder.HasConversion(value => value.Value, value => new LastName(value));

    public static PropertyBuilder<Biography?> HasBiographyConversion(this PropertyBuilder<Biography?> builder)
        => builder.HasConversion<string?>(
            value => value == null ? null : value.Value,
            value => value == null ? null : new Biography(value));

    public static PropertyBuilder<Experience?> HasExperienceConversion(this PropertyBuilder<Experience?> builder)
        => builder.HasConversion<int?>(
            value => value == null ? null : value.Value,
            value => value.HasValue ? new Experience(value.Value) : null);

    public static PropertyBuilder<CaseTitle> HasCaseTitleConversion(this PropertyBuilder<CaseTitle> builder)
        => builder.HasConversion(value => value.Value, value => new CaseTitle(value));

    public static PropertyBuilder<CaseDescription> HasCaseDescriptionConversion(this PropertyBuilder<CaseDescription> builder)
        => builder.HasConversion(value => value.Value, value => new CaseDescription(value));

    public static PropertyBuilder<ClaimContent> HasClaimContentConversion(this PropertyBuilder<ClaimContent> builder)
        => builder.HasConversion(value => value.Value, value => new ClaimContent(value));

    public static PropertyBuilder<CommentContent> HasCommentContentConversion(this PropertyBuilder<CommentContent> builder)
        => builder.HasConversion(value => value.Value, value => new CommentContent(value));

    public static PropertyBuilder<ProposalContent> HasProposalContentConversion(this PropertyBuilder<ProposalContent> builder)
        => builder.HasConversion(value => value.Value, value => new ProposalContent(value));

    public static PropertyBuilder<VerdictContent> HasVerdictContentConversion(this PropertyBuilder<VerdictContent> builder)
        => builder.HasConversion(value => value.Value, value => new VerdictContent(value));

    public static PropertyBuilder<RuleTitle> HasRuleTitleConversion(this PropertyBuilder<RuleTitle> builder)
        => builder.HasConversion(value => value.Value, value => new RuleTitle(value));

    public static PropertyBuilder<RuleContent> HasRuleContentConversion(this PropertyBuilder<RuleContent> builder)
        => builder.HasConversion(value => value.Value, value => new RuleContent(value));
}
