using AutoMapper;
using MediatR;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.CustomerFeatures.AddCustomer
{
    public class AddCustomerHandler : IRequestHandler<AddCustomerRequest, AddCustomerResponse>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddCustomerHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddCustomerResponse> Handle(AddCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);
            var vehicles = request.VehicleDTOs.Select(dto => _mapper.Map<Vehicle>(dto)).ToList();
            int i = 0;
            foreach (var vehicle in vehicles)
            {
                vehicle.Id = Guid.NewGuid();
                vehicle.Code = await _customerRepository.GetNewVehicleCode() + i;
                i++;
            }
            customer.Code = await _customerRepository.GetNewCustomerCode();
            customer.Vehicles = vehicles;
            await _customerRepository.AddAsync(customer);
            await _unitOfWork.SaveAsync(cancellationToken);
            var response = new AddCustomerResponse(customer.Id, customer.Vehicles.Select(a => a.Id).ToList());
            return response;
        }
    }
}
