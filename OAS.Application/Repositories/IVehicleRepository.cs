using OAS.Application.Features.VehicleFeatures.GetVehiclesMinimal;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Repositories
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAllAsync();
        //Task<List<GetVehiclesMinimal_DTO>> Get();
        Task<Vehicle?> GetByIdAsync(Guid id);
    }
}
