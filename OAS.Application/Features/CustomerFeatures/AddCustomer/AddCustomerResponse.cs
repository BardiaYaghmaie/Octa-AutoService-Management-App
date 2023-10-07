using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.CustomerFeatures.AddCustomer
{
    public sealed record AddCustomerResponse(Guid CustomerId , List<Guid> VehicleIds);    
}
