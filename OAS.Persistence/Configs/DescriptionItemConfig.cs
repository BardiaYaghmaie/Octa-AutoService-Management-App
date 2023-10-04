using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAS.Domain.Models;

namespace OAS.Persistence.Configs;

public class DescriptionItemConfig : IEntityTypeConfiguration<DescriptionItem>
{
    public void Configure(EntityTypeBuilder<DescriptionItem> builder)
    {
        builder.ToTable("DescriptionItem");

        builder.HasKey(a => a.Id);
        builder.Property(a=> a.Title).HasMaxLength(255).IsRequired(true);
        builder.HasMany(a => a.InvoiceDescriptions)
            .WithOne(a => a.DescriptionItem)
            .HasPrincipalKey(a => a.Id)
            .HasForeignKey(a => a.DescriptionItemId);
    }
}
