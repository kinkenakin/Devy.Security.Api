using Devy.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Devy.Security.Domain.Configurations;

public class LocationEntityConfig : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        builder.HasKey(e => e.Id);

        builder.Property(e => e.City)
            .HasColumnName("City");

        builder.Property(e => e.Country)
            .HasColumnName("Country");
    }
}
