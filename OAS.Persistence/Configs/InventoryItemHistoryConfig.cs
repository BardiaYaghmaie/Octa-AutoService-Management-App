using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAS.Domain.Models;

namespace OAS.Persistence.Configs;

public class InventoryItemHistoryConfig : IEntityTypeConfiguration<InventoryItemHistory>
{
    public void Configure(EntityTypeBuilder<InventoryItemHistory> builder)
    {
        builder.ToTable("InventoryItemHistory");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.InventoryItemId).IsRequired(true);
        builder.Property(a => a.IsActive).IsRequired(true);
        builder.Property(a => a.Name).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.SellerName).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.BuyFactorCode).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.Code).IsRequired(true);
        builder.Property(a => a.RegisterDate).IsRequired(true);
        builder.Property(a => a.CountLowerBound).IsRequired(true);
        builder.Property(a => a.BuyPrice).IsRequired(true);
        builder.Property(a => a.SellPrice).IsRequired(true);        
    }
}


