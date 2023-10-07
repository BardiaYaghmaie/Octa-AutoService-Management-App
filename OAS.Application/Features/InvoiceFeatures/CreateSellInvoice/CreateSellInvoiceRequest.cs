using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.CreateInvoice
{
    public sealed record CreateSellInvoiceRequest(Guid VehicleId):IRequest<CreateSellInvoiceResponse>;    
}
