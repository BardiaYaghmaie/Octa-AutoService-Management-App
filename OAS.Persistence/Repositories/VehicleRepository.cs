using Microsoft.EntityFrameworkCore;
using OAS.Application.Features.VehicleFeatures.GetVehiclesMinimal;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using OAS.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Persistence.Repositories
{
    internal class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public VehicleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task <List<Vehicle>> GetAllAsync()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }
        //public async Task<List<GetVehiclesMinimal_DTO>> Get()
        //{
        //    var data = await _dbContext.Vehicles.Select(a => new GetVehiclesMinimal_DTO(a.Id, a.Code, a.Name)).ToListAsync();
        //    return data;
        //}

        public Task<Vehicle?> GetByIdAsync(Guid id)
        {
            return _dbContext.Vehicles.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
