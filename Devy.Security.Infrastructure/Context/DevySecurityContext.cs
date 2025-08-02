using Microsoft.EntityFrameworkCore;
using Devy.Security.Domain.Entities;
using Devy.Security.Domain.Configurations;
namespace Devy.Security.Infrastructure;

public class DevySecurityContext : DbContext
{
    public DevySecurityContext(DbContextOptions<DevySecurityContext> options)
    : base(options)
    {
    }

    public virtual DbSet<EmployeeContact> EmployeeContacts { get; set; }
    public virtual DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
            throw new ArgumentNullException(nameof(modelBuilder));

        modelBuilder.ApplyConfiguration(new EmployeeContactEntityConfig());
        modelBuilder.ApplyConfiguration(new LocationEntityConfig());
        OnModelCreatingPartial(modelBuilder);
    }
    public void OnModelCreatingPartial(ModelBuilder modelBuilder) { }
}