using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetSellInvoices
{
    public sealed class GetSellInvoicesHandler : IRequestHandler<GetSellInvoicesRequest, GetSellInvoicesResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInventoryItemRepository  _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository  _inventoryItemHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetSellInvoicesHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork, IInventoryItemRepository inventoryItemRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _inventoryItemRepository = inventoryItemRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
        }

        public async Task<GetSellInvoicesResponse> Handle(GetSellInvoicesRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepository.GetSellInvoicesAsync();
            var inventoryItemHistories = await _inventoryItemHistoryRepository.GetAllAsync();
            var invoicePayments = await _invoiceRepository.GetAllInvoicePaymentsAsync();
            var invoiceInventoryItems = await _invoiceRepository.GetAllInvoiceInventoryItemsAsync();
            var data = invoices.Where(a => a.Type == Domain.Enums.InvoiceType.Sell).Select((a, i) =>
            {
                long paidAmount = invoicePayments.Where(b => b.InvoiceId == a.Id).Select(b => b.PaidAmount).Sum();
                float total = 0;
                foreach (var invoiceInventoryItem in invoiceInventoryItems.Where(b => b.InvoiceId == a.Id).ToList())
                {
                    float count = invoiceInventoryItem.Count;
                    DateTime registerDate = invoiceInventoryItem.RegisterDate;
                    var inventoryItemId = invoiceInventoryItem.InventoryItemId;
                    var buyPrice = inventoryItemHistories.Where(b => b.InventoryItemId == inventoryItemId && b.BuyPrice.HasValue && b.UpdateDate <= registerDate).OrderByDescending(b => b.UpdateDate).Select(b => a.UseBuyPrice.HasValue && a.UseBuyPrice.Value ? b.BuyPrice : b.SellPrice.Value).FirstOrDefault() ?? 0;
                    total += (count * buyPrice);
                }

                return new GetSellInvoices_InvoiceDTO
                (
                    InvoiceCode: a.Code.ToString(),
                    InvoiceDate: a.RegisterDate,
                    InvoiceDateString: a.RegisterDate.ToString(),
                    InvoiceId: a.Id,
                    RowNumber: i + 1,
                    InvoiceTotalPrice: total,
                    InvoicePaidAmount: paidAmount,
                    VehicleName: a.Vehicle?.Name,
                    CustomerName: a.VehicleId.HasValue ? (a.Vehicle.Customer.FirstName + " " + a.Vehicle.Customer.LastName) : (a.CustomerId.HasValue ? (a.Customer.FirstName + " " + a.Customer.LastName) : (""))

                );
            }).ToList();
            var response = new GetSellInvoicesResponse(Data: data);
            return response;
        }
    }
}
