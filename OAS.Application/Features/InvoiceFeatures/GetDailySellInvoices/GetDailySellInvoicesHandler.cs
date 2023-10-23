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
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;
        private readonly IServiceHistoryRepository _serviceHistoryRepository;

        public GetDailySellInvoicesHandler(IInvoiceRepository invoiceRepository, IServiceHistoryRepository serviceHistoryRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _serviceHistoryRepository = serviceHistoryRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
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


        public async Task<GetDailySellInvoicesResponse> Handle(GetDailySellInvoicesRequest request, CancellationToken cancellationToken)
        {
            var data = await _invoiceRepository.GetDailySellInvoicesAsync();
            var data1 = data.Where(a => a.VehicleId.HasValue || a.CustomerId.HasValue)
            .Select(async (a, i) => new GetDailySellInvoices_DTO(
                a.Id,
                a.Code.ToString(),
                a.VehicleId.HasValue ? a.Vehicle.Name : "",
                a.VehicleId.HasValue ? (a.Vehicle.Customer.FirstName + " " + a.Vehicle.Customer.LastName) : a.CustomerId.HasValue ? (a.Customer.FirstName + " " + a.Customer.LastName) : "",
                i + 1,
                
                 await this.CalculateInvoiceTotalCost(a.Id)

            )).ToList();
            var x = (await Task.WhenAll(data1)).ToList();
            var response = new GetDailySellInvoicesResponse(x);
            return response;
        }
    }
}
