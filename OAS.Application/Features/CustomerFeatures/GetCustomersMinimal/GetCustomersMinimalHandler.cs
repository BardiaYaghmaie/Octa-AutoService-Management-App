using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.CustomerFeatures.GetCustomersMinimal
{
    public sealed class GetCustomersMinimalHandler : IRequestHandler<GetCustomersMinimalRequest, GetCustomersMinimalResponse>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomersMinimalHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomersMinimalResponse> Handle(GetCustomersMinimalRequest request, CancellationToken cancellationToken)
        {
            var data =(await  _customerRepository.GetAllAsync()).Select(a => new GetCustomersMinimal_DTO(a.Id, a.Code, a.FirstName + " " + a.LastName)).ToList();
            var response = new GetCustomersMinimalResponse(data);
            return response;
        }
    }
}
