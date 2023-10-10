using MediatR;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetDailySellInvoices
{
    public sealed record GetDailySellInvoicesRequest() : IRequest<GetDailySellInvoicesResponse>;
}
