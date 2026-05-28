using AC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.EntityFramework.Configurations;

public class SettlementProposalConfiguration : IEntityTypeConfiguration<SettlementProposal>
{
    public void Configure(EntityTypeBuilder<SettlementProposal> builder)
    {
        builder.ToTable("SettlementProposals");
        builder.ConfigureEntity();
        builder.Property(proposal => proposal.Content).HasProposalContentConversion().HasMaxLength(1000).IsRequired();
        builder.Property(proposal => proposal.Status).HasConversion<string>().HasMaxLength(32).IsRequired();
        builder.Property<Guid>("CaseId").IsRequired();
        builder.HasIndex("CaseId");
        builder.HasOne(proposal => proposal.Defendant).WithMany().HasForeignKey("DefendantId").OnDelete(DeleteBehavior.Restrict).IsRequired();
    }
}
