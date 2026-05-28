using AC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.EntityFramework.Configurations;

public class ArbitratorConfiguration : IEntityTypeConfiguration<Arbitrator>
{
    public void Configure(EntityTypeBuilder<Arbitrator> builder)
    {
        builder.ToTable("Arbitrators");
        builder.ConfigureEntity();
        builder.Property(arbitrator => arbitrator.Name).HasFirstNameConversion().HasColumnName("FirstName").HasMaxLength(50).IsRequired();
        builder.Property(arbitrator => arbitrator.Surname).HasLastNameConversion().HasColumnName("LastName").HasMaxLength(50).IsRequired();
        builder.Property(arbitrator => arbitrator.Bio).HasBiographyConversion().HasMaxLength(1000);
        builder.Property(arbitrator => arbitrator.Exp).HasExperienceConversion();
        builder.Property(arbitrator => arbitrator.IsActived).IsRequired();
    }
}
