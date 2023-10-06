using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.DeleteService
{
    public sealed record DeleteServiceRequest(int Code):IRequest<DeleteServiceResponse>;    
}
