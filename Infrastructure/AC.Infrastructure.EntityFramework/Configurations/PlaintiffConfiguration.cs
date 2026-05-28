using AC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.EntityFramework.Configurations;

public class PlaintiffConfiguration : IEntityTypeConfiguration<Plaintiff>
{
    public void Configure(EntityTypeBuilder<Plaintiff> builder)
    {
        builder.ToTable("Plaintiffs");
        builder.ConfigureEntity();
        builder.Property(plaintiff => plaintiff.Name).HasFirstNameConversion().HasColumnName("FirstName").HasMaxLength(50).IsRequired();
        builder.Property(plaintiff => plaintiff.Surname).HasLastNameConversion().HasColumnName("LastName").HasMaxLength(50).IsRequired();
    }
}
