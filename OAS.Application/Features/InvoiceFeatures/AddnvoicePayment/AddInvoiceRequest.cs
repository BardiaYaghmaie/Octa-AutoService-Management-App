using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.AddSellInvoicePayment
{
    public sealed record AddInvoiceRequest(Guid InvoiceId , List<Tuple<string ,long>> TrackCodeAndAmountList ) :IRequest<AddInvoiceResponse>;/* List<Tuple<string ,long> TrackCodeAndAmountList = string TrackCode , long Amount*/
}
