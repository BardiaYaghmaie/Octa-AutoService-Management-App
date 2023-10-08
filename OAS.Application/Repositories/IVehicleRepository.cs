using OAS.Application.Features.VehicleFeatures.GetVehiclesMinimal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Repositories
{
    public interface IVehicleRepository
    {
        Task<List<GetVehiclesMinimal_DTO>> Get();
    }
}
