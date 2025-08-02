using Devy.Security.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Devy.Security.Domain.Configurations;

public class EmployeeContactEntityConfig : IEntityTypeConfiguration<EmployeeContact>
{
    public void Configure(EntityTypeBuilder<EmployeeContact> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        builder.ToTable("Employee_Contacts");

        builder.HasKey(e => e.EmployeeId);

        builder.Property(e => e.EmployeeId)
            .HasColumnName("Employee_Id");

        builder.Property(e => e.CountryCode)
            .HasColumnName("Country_Code");

        builder.Property(e => e.LocationId)
            .HasColumnName("Location_Id");
    }
}
