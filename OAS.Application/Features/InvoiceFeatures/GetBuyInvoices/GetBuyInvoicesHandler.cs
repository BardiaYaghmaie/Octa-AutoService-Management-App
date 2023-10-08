using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetBuyInvoices
{
    public sealed class GetBuyInvoicesHandler : IRequestHandler<GetBuyInvoicesRequest, GetBuyInvoicesResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetBuyInvoicesHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<GetBuyInvoicesResponse> Handle(GetBuyInvoicesRequest request, CancellationToken cancellationToken)
        {
            var data = await _invoiceRepository.GetBuyInvoicesAsync();
            var response = new GetBuyInvoicesResponse(Data:data);
            return response;
        }
    }
}
