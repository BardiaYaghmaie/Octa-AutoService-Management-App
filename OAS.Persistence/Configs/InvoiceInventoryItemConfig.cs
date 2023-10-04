using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAS.Domain.Models;

namespace OAS.Persistence.Configs;

public class InvoiceInventoryItemConfig : IEntityTypeConfiguration<InvoiceInventoryItem>
{

    public void Configure(EntityTypeBuilder<InvoiceInventoryItem> builder)
    {
        builder.ToTable("InvoiceInventoryItem");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.InvoiceId).IsRequired(true);
        builder.Property(a => a.InventoryItemId).IsRequired(true);
    }
}
