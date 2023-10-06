using MediatR;
using OAS.Application.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.CustomerFeatures.AddCustomer
{
    public sealed record AddCustomerRequest(string FirstName , string LastName , string phoneNumber,
        DateTime RegisterDate , List<VehicleDTO> VehicleDTOs):IRequest<AddCustomerResponse>;   
}
