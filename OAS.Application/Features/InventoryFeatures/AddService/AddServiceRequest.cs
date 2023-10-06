using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.AddService
{
    public sealed record AddServiceRequest(string Name , long DefaultPrice):IRequest<AddServiceResponse>;    
}
