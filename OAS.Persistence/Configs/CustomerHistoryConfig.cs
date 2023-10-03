﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OAS.Domain.Models;

namespace OAS.Persistence.Configs;

public class CustomerHistoryConfig : IEntityTypeConfiguration<CustomerHistory>
{
    public void Configure(EntityTypeBuilder<CustomerHistory> builder)
    {        
        builder.HasKey(a => a.Id);
        builder.Property(a => a.CustomerId).IsRequired(true);
        builder.Property(a => a.FirstName).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.LastName).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.PhoneNumber).HasMaxLength(255).IsRequired(true);
        builder.Property(a => a.Code).IsRequired(true);
        builder.Property(a => a.UpdateDate).IsRequired(true);
    }
}
