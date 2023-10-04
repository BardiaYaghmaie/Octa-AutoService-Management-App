using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAS.Domain.Models;

namespace OAS.Persistence.Configs;

public class ServiceHistoryConfig : IEntityTypeConfiguration<ServiceHistory>
{
    public void Configure(EntityTypeBuilder<ServiceHistory> builder)
    {
        builder.ToTable("ServiceHistory");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.ServiceId).IsRequired(true);
        builder.Property(a => a.Name).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.IsActive).IsRequired(true);
        builder.Property(a => a.UpdateDate).IsRequired(true);

     
    }
}