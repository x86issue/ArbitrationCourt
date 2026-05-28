using AC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.EntityFramework.Configurations;

public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.ToTable("Claims");
        builder.ConfigureEntity();
        builder.Property(claim => claim.Content).HasClaimContentConversion().HasMaxLength(1000).IsRequired();
        builder.Property<Guid>("CaseId").IsRequired();
        builder.HasIndex("CaseId").IsUnique();
        builder.HasOne(claim => claim.Plaintiff).WithMany().HasForeignKey("PlaintiffId").OnDelete(DeleteBehavior.Restrict).IsRequired();
        builder.HasOne(claim => claim.Defendant).WithMany().HasForeignKey("DefendantId").OnDelete(DeleteBehavior.Restrict).IsRequired();
    }
}
