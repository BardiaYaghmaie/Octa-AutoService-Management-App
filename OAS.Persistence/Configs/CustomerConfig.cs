using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Persistence.Configs;
public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.FirstName).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.LastName).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.PhoneNumber).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.Code).IsRequired(true);
        builder.Property(a => a.RegisterDate).IsRequired(true);

        builder.HasMany(a => a.CustomerHistories)
            .WithOne(a => a.Customer)
            .HasPrincipalKey(a => a.Id)
            .HasForeignKey(a => a.CustomerId);
    }
}
