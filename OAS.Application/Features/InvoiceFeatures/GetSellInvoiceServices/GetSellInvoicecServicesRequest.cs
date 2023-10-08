using MediatR;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetSellInvoiceServices
{
    public sealed record GetSellInvoicecServicesRequest(Guid InvoiceId):IRequest<GetSellInvoiceServicesResponse>;    
}
