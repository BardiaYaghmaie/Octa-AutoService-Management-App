using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OAS.Application.Features.Inventory.GetServices
{
    public record GetServicesRequest:IRequest<GetServicesResponse>
    {
        
    }
}
