using AC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AC.Infrastructure.EntityFramework.Configurations;

public class DefendantConfiguration : IEntityTypeConfiguration<Defendant>
{
    public void Configure(EntityTypeBuilder<Defendant> builder)
    {
        builder.ToTable("Defendants");
        builder.ConfigureEntity();
        builder.Property(defendant => defendant.Name).HasFirstNameConversion().HasColumnName("FirstName").HasMaxLength(50).IsRequired();
        builder.Property(defendant => defendant.Surname).HasLastNameConversion().HasColumnName("LastName").HasMaxLength(50).IsRequired();
    }
}
