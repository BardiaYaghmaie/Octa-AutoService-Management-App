﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAS.Domain.Models;

namespace OAS.Persistence.Configs;

public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoice");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.IssueDate).IsRequired(true);
        builder.Property(a => a.RegisterDate).IsRequired(true);
        builder.Property(a => a.DiscountAmount).IsRequired(false);
        builder.Property(a => a.VehicleId).IsRequired(false);
        builder.Property(a => a.CustomerId).IsRequired(false);
        builder.Property(a => a.Code).IsRequired(true);
        builder.Property(a => a.UseBuyPrice).IsRequired(false);
        builder.Property(a => a.SerllerName).IsRequired(false);
        builder.Property(a => a.Type).IsRequired(true);

        builder.HasMany(a => a.InvoiceServiceItems)
            .WithOne(a => a.Invoice)
            .HasPrincipalKey(a => a.Id)
            .HasForeignKey(a => a.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(a => a.InvoiceDescriptions)
            .WithOne(a => a.Invoice)
          .HasPrincipalKey(a => a.Id)
         .HasForeignKey(a => a.InvoiceId)
         .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(a => a.InvoiceServiceItems)
    .WithOne(a => a.Invoice)
  .HasPrincipalKey(a => a.Id)
 .HasForeignKey(a => a.InvoiceId)
         .OnDelete(DeleteBehavior.Cascade);




        builder.HasMany(a => a.InvoicePayments)
.WithOne(a => a.Invoice)
.HasPrincipalKey(a => a.Id)
.HasForeignKey(a => a.InvoiceId)
         .OnDelete(DeleteBehavior.Cascade);



    }
}
