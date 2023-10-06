using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OAS.Application.DomainModels;

namespace OAS.Application.Features.Inventory.GetServices
{
    public sealed class GetServicesHandler : IRequestHandler<GetServicesRequest, GetServicesResponse>
    {
        private readonly IServiceRepository  _serviceRepository;

        public GetServicesHandler(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<GetServicesResponse> Handle(GetServicesRequest request, CancellationToken cancellationToken)
        {
            var services = await _serviceRepository.GetAllAsync();
            var serviceDTOs = services.Select((item, index) =>
            {
                return new ServiceDTO(
                    RowNumber: index + 1,
                    Code: item.Code.ToString(),
                    Title: item.Name,
                    Price: item.DefaultPrice
                    );
            }).ToList();
            var response = new GetServicesResponse(serviceDTOs);
            return response;
        }
    }
}
