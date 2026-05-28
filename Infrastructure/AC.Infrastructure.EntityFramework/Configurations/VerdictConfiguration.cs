using AC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.EntityFramework.Configurations;

public class VerdictConfiguration : IEntityTypeConfiguration<Verdict>
{
    public void Configure(EntityTypeBuilder<Verdict> builder)
    {
        builder.ToTable("Verdicts");
        builder.ConfigureEntity();
        builder.Property(verdict => verdict.Content).HasVerdictContentConversion().HasMaxLength(1000).IsRequired();
        builder.Property<Guid>("CaseId").IsRequired();
        builder.HasIndex("CaseId").IsUnique();
        builder.HasOne(verdict => verdict.Arbitrator).WithMany().HasForeignKey("ArbitratorId").OnDelete(DeleteBehavior.Restrict).IsRequired();
    }
}
