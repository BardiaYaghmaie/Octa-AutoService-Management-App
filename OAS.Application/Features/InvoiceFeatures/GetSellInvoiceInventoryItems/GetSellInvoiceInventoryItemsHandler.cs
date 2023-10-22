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
            var answer = data.Select((a, i) => new GetSellInvoiceInventoryItems_DTO
            (
                RowNumber: i + 1,
                InventoryItemCode: a.InventoryItem.Code.ToString(),
                InvoiceInventoryItemId: a.Id,
                InventoryItemId: a.InventoryItemId,
                InventoryItemName: a.InventoryItem.Name,
                InventoryItemCount: a.Count,
                UnitBuyPrice: a.InventoryItem.BuyPrice.Value,
                UnitSellPrice: a.InventoryItem.SellPrice.Value,
                TotalPrice: a.Count * a.InventoryItem.SellPrice.Value
            )).ToList();
            var response = new GetSellInvoiceInventoryItemsResponse(Data: answer);
            return response;
        
        }
    }
}
