using MediatR;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.CustomerFeatures.GetCustomers
{
    public sealed class GetCustomersHandler : IRequestHandler<GetCustomersRequest, GetCustomersResponse>
    {
        private ICustomerRepository _customerRepository;

        public GetCustomersHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<GetCustomersResponse> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var data = (await _customerRepository.GetAllAsync()).Select((a, i) => new GetCustomersResponse_DTO
            {
                Code = a.Code.ToString(),
                FirstName = a.FirstName,
                LastName = a.LastName,
                RowNumber = i + 1,
                CustomerPhoneNumber = a.PhoneNumber
            }).ToList();
            var response = new GetCustomersResponse
            {
                Count = data.Count(),
                Data = data
            };
            return response;
        }
    }
}
