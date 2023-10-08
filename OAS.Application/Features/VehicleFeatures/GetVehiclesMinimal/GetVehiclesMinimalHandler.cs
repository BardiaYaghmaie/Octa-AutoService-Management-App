using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.VehicleFeatures.GetVehiclesMinimal
{
    public sealed class GetVehiclesMinimalHandler : IRequestHandler<GetVehiclesMinimalRequest, GetVehiclesMinimalResponse>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehiclesMinimalHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<GetVehiclesMinimalResponse> Handle(GetVehiclesMinimalRequest request, CancellationToken cancellationToken)
        {
            var data = await _vehicleRepository.Get();
            var response = new GetVehiclesMinimalResponse(data);
            return response;
        }
    }
}
