using AC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.EntityFramework.Configurations;

public class CourtRuleConfiguration : IEntityTypeConfiguration<CourtRule>
{
    public void Configure(EntityTypeBuilder<CourtRule> builder)
    {
        builder.ToTable("CourtRules");
        builder.ConfigureEntity();
        builder.Property(rule => rule.Title).HasRuleTitleConversion().HasMaxLength(100).IsRequired();
        builder.Property(rule => rule.Description).HasRuleContentConversion().HasMaxLength(1000).IsRequired();
    }
}
