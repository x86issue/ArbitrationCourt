using AC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.EntityFramework.Configurations;

public class CaseConfiguration : IEntityTypeConfiguration<Case>
{
    public void Configure(EntityTypeBuilder<Case> builder)
    {
        builder.ToTable("Cases");
        builder.ConfigureEntity();

        builder.Property(@case => @case.Title).HasCaseTitleConversion().HasMaxLength(200).IsRequired();
        builder.Property(@case => @case.Description).HasCaseDescriptionConversion().HasMaxLength(1000).IsRequired();
        builder.Property(@case => @case.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property(@case => @case.ClosedAt);

        builder.HasOne(@case => @case.Plaintiff)
            .WithMany()
            .HasForeignKey("PlaintiffId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(@case => @case.Defendant)
            .WithMany()
            .HasForeignKey("DefendantId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(@case => @case.Arbitrator)
            .WithMany()
            .HasForeignKey("ArbitratorId")
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(@case => @case.Claim)
            .WithOne()
            .HasForeignKey<Claim>("CaseId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(@case => @case.Verdict)
            .WithOne(verdict => verdict.CaseAssigned)
            .HasForeignKey<Verdict>("CaseId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(@case => @case.Comments)
            .WithOne(comment => comment.CaseAssigned)
            .HasForeignKey("CaseId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(@case => @case.Proposals)
            .WithOne(proposal => proposal.CaseAssigned)
            .HasForeignKey("CaseId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(@case => @case.Rules)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "CaseCourtRules",
                right => right.HasOne<CourtRule>().WithMany().HasForeignKey("CourtRuleId").OnDelete(DeleteBehavior.Cascade),
                left => left.HasOne<Case>().WithMany().HasForeignKey("CaseId").OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.HasKey("CaseId", "CourtRuleId");
                    join.ToTable("CaseCourtRules");
                });

        builder.Navigation(@case => @case.Comments).HasField("_comments").UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(@case => @case.Proposals).HasField("_proposals").UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(@case => @case.Rules).HasField("_rules").UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
