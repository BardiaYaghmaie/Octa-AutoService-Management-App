using MediatR;
using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetSellInvoices
{
    public sealed class GetSellInvoicesHandler : IRequestHandler<GetSellInvoicesRequest, GetSellInvoicesResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInventoryItemRepository  _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository  _inventoryItemHistoryRepository;
        private readonly IServiceHistoryRepository _serviceHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetSellInvoicesHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork, IInventoryItemRepository inventoryItemRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository, IServiceHistoryRepository serviceHistoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _inventoryItemRepository = inventoryItemRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
            _serviceHistoryRepository = serviceHistoryRepository;
        }
        private async Task<float> CalculateInvoiceTotalCost(Guid invoiceId)
        {
            float total = 0;
            var invoice = await _invoiceRepository.GetById(invoiceId);
            bool invoiceUseBuyPrice = invoice.UseBuyPrice.HasValue ? invoice.UseBuyPrice.Value : false;
            var invoiceInventoryItems = await _invoiceRepository.GetInvoiceInventoryItemsByInvoiceId(invoiceId);
            var invoiceServiceItems = await _invoiceRepository.GetInvoiceServicesByInvoiceIdAsync(invoiceId);
            foreach (var item in invoiceInventoryItems)
            {
                var inventoryItemHistory = await _inventoryItemHistoryRepository.GetLatestByInventoryItemIdAndDateAsync(item.InventoryItemId, item.RegisterDate);
                total += invoiceUseBuyPrice ? (item.Count * inventoryItemHistory.BuyPrice.Value) : (item.Count * inventoryItemHistory.SellPrice.Value);
            }

            foreach (var item in invoiceServiceItems)
            {
                var serviceHistory = await _serviceHistoryRepository.GetLatestServiceHistoryByServiceIdAndDate(item.ServiceId, item.RegisterDate);
                total += (1 * item.Price);
            }
            return total;
        }

        public async Task<GetSellInvoicesResponse> Handle(GetSellInvoicesRequest request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepository.GetSellInvoicesAsync();
            var inventoryItemHistories = await _inventoryItemHistoryRepository.GetAllAsync();
            var invoicePayments = await _invoiceRepository.GetAllInvoicePaymentsAsync();
            var invoiceInventoryItems = await _invoiceRepository.GetAllInvoiceInventoryItemsAsync();
            var data = invoices.Where(a => a.Type == Domain.Enums.InvoiceType.Sell).Select(async (a, i) =>
            {
                long paidAmount = invoicePayments.Where(b => b.InvoiceId == a.Id).Select(b => b.PaidAmount).Sum();
                float total = await this.CalculateInvoiceTotalCost(a.Id);

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
            var response = new GetSellInvoicesResponse(Data: (await Task.WhenAll(data)).ToList());
            return response;
        }
    }
}
