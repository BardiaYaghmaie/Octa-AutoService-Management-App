using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAS.Domain.Models;

namespace OAS.Persistence.Configs;

public class ServiceConfig : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Service");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.IsActive).IsRequired(true);
        builder.Property(a => a.RegisterDate).IsRequired(true);

        builder.HasMany(a => a.InvoiceServices)
            .WithOne(a => a.Service)
            .HasPrincipalKey(a => a.Id)
            .HasForeignKey(a => a.ServiceId);

        builder.HasMany(a => a.ServiceHistories)
            .WithOne(a => a.Service)
            .HasPrincipalKey(a => a.Id)
            .HasForeignKey(a => a.ServiceId);
    }
}
