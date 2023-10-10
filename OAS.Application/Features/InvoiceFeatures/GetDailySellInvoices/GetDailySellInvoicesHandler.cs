using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetDailySellInvoices
{
    public sealed record GetDailySellInvoicesHandler : IRequestHandler<GetDailySellInvoicesRequest, GetDailySellInvoicesResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetDailySellInvoicesHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<GetDailySellInvoicesResponse> Handle(GetDailySellInvoicesRequest request, CancellationToken cancellationToken)
        {
            var data = await _invoiceRepository.GetDailySellInvoicesAsync();
            var response = new GetDailySellInvoicesResponse(data);
            return response;
        }
    }
}
