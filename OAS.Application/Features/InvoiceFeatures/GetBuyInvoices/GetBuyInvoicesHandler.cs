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
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;

        public GetBuyInvoicesHandler(IInvoiceRepository invoiceRepository, IInventoryItemRepository inventoryItemRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _inventoryItemRepository = inventoryItemRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
        }

        public async Task<GetBuyInvoicesResponse> Handle(GetBuyInvoicesRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            var inventoryItemHistories = await _inventoryItemHistoryRepository.GetAllAsync();
            var invoicePayments = await _invoiceRepository.GetAllInvoicePaymentsAsync();
            var invoiceInventoryItems = await _invoiceRepository.GetAllInvoiceInventoryItemsAsync();
            var data = invoices.Where(a => a.Type == Domain.Enums.InvoiceType.Buy).Select((a, i) =>
            {
                long paidAmount = invoicePayments.Where(b => b.InvoiceId == a.Id).Select(b => b.PaidAmount).Sum();
                float total = 0;
                foreach (var invoiceInventoryItem in invoiceInventoryItems.Where(b => b.InvoiceId == a.Id).ToList())
                {
                    float count = invoiceInventoryItem.Count;
                    DateTime registerDate = invoiceInventoryItem.RegisterDate;
                    var inventoryItemId = invoiceInventoryItem.InventoryItemId;
                    var buyPrice = inventoryItemHistories.Where(b => b.InventoryItemId == inventoryItemId && b.BuyPrice.HasValue && b.UpdateDate <= registerDate).OrderByDescending(b => b.UpdateDate).Select(b => b.BuyPrice.Value).First();
                    total += (count * buyPrice);
                }

                return new GetBuyInvoices_InvoiceDTO
                (
                    SellerName: a.SerllerName,
                    InvoiceCode: a.Code.ToString(),
                    InvoiceDate: a.RegisterDate,
                    InvoiceDateString: a.RegisterDate.ToString(),
                    InvoiceId: a.Id,
                    RowNumber: i + 1,
                    InvoiceTotalPrice: total,
                    InvoicePaidAmount: paidAmount

                );
            }).ToList();
            var response = new GetBuyInvoicesResponse(Data:data);
            return response;
            //var data = await _invoiceRepository.GetBuyInvoicesAsync();
            //return response;
        }
    }
}
