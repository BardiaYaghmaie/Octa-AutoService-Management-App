using Microsoft.EntityFrameworkCore;
using OAS.Domain.Models;
using OAS.Persistence.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Persistence.Contexts;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CustomerHistory> CustomerHistories { get; set; }
    public virtual DbSet<DescriptionItem> DescriptionItems { get; set; }
    public virtual DbSet<InventoryItem> InventoryItems { get; set; }
    public virtual DbSet<InventoryItemHistory> InventoryItemHistories { get; set; }
    public virtual DbSet<Invoice> InvoiceItems { get; set; }
    public virtual DbSet<InvoiceDescription> InvoiceDescriptions { get; set; }
    public virtual DbSet<InvoiceInventoryItem> InvoiceInventoryItems { get; set; }
    public virtual DbSet<InvoicePayment> InvoicePayments { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public virtual DbSet<ServiceHistory> ServiceHistories { get; set; }
    public virtual DbSet<Vehicle> Vehicles { get; set; }
    public virtual DbSet<VehicleHistory> VehicleHistories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CustomerConfig());
        modelBuilder.ApplyConfiguration(new CustomerHistoryConfig());
        modelBuilder.ApplyConfiguration(new DescriptionItemConfig());
        modelBuilder.ApplyConfiguration(new InventoryItemConfig());
        modelBuilder.ApplyConfiguration(new InventoryItemHistoryConfig());
        modelBuilder.ApplyConfiguration(new InvoiceConfig());
        modelBuilder.ApplyConfiguration(new InvoiceDescriptionConfig());
        modelBuilder.ApplyConfiguration(new InvoiceInventoryItemConfig());
        modelBuilder.ApplyConfiguration(new ServiceConfig());
        modelBuilder.ApplyConfiguration(new ServiceHistoryConfig());
        modelBuilder.ApplyConfiguration(new VehicleConfig());
        modelBuilder.ApplyConfiguration(new VehicleHistoryConfig());
    }
}
