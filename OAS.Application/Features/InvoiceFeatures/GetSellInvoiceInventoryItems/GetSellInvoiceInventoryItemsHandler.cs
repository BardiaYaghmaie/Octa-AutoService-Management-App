using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems
{
    public sealed class GetSellInvoiceInventoryItemsHandler : IRequestHandler<GetSellInvoiceInventoryItemsRequest, GetSellInvoiceInventoryItemsResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetSellInvoiceInventoryItemsHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<GetSellInvoiceInventoryItemsResponse> Handle(GetSellInvoiceInventoryItemsRequest request, CancellationToken cancellationToken)
        {
            var data = await _invoiceRepository.GetSellInvoiceInventoryItemsAsync(request.InvoiceId);
            var response = new GetSellInvoiceInventoryItemsResponse(Data:data);
            return response;
        
        }
    }
}
