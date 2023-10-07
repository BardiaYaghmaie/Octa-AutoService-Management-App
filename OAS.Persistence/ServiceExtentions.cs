using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OAS.Application.Repositories;
using OAS.Persistence.Contexts;
using OAS.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Persistence;
public static class ServiceExtentions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("OAS");

        services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString) , ServiceLifetime.Singleton);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IInventoryItemHistoryRepository, InventoryItemHistoryRepository>();
        services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IServiceHistoryRepository, ServiceHistoryRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

    }
}
