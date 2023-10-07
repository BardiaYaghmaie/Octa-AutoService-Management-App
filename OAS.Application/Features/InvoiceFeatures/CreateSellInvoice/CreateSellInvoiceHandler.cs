using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.CreateInvoice
{
    internal class CreateSellInvoiceHandler : IRequestHandler<CreateSellInvoiceRequest, CreateSellInvoiceResponse>
    {
        public async Task<CreateSellInvoiceResponse> Handle(CreateSellInvoiceRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
