using MediatR;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.AddSellInvoicePayment
{
    public sealed class AddInvoiceHandler : IRequestHandler<AddInvoiceRequest, AddInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInventoryItemHistoryRepository  _inventoryItemHistoryRepository;
        private readonly IServiceHistoryRepository  _serviceHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddInvoiceHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork, IInventoryItemHistoryRepository inventoryItemHistoryRepository, IServiceHistoryRepository serviceHistoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
            _serviceHistoryRepository = serviceHistoryRepository;
        }

        public async Task<AddInvoiceResponse> Handle(AddInvoiceRequest request, CancellationToken cancellationToken)
        {
            Invoice? invoice = await _invoiceRepository.GetById(request.InvoiceId);
            if (invoice == null)
                throw new Exception("");
            bool invoiceUseBuyPrice = invoice.UseBuyPrice.HasValue ? invoice.UseBuyPrice.Value : false;
            List<InvoicePayment> invoicePayments = request.TrackCodeAndAmountList.Select(a => new InvoicePayment
            {
                InvoiceId = invoice.Id,
                Id = Guid.NewGuid(),
                TrackCode = a.Item1,
                PaidAmount = a.Item2,
                LastPaymentDate = DateTime.UtcNow
            }).ToList();

            #region checking wether the payments are valid or not
            float total = 0;
            var invoiceInventoryItems = await _invoiceRepository.GetInvoiceInventoryItemsByInvoiceId(request.InvoiceId);
            var invoiceServiceItems = await _invoiceRepository.GetInvoiceServicesByInvoiceIdAsync(request.InvoiceId);
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
            var previousInvoicePayments = await _invoiceRepository.GetInvoicePaymentsByInvoiceIdAsync(invoice.Id);
            var paidSoFar = previousInvoicePayments.Sum(a => a.PaidAmount);
            var newPaidAmount = invoicePayments.Sum(a => a.PaidAmount);
            if (paidSoFar + newPaidAmount > total) throw new Exception("");            
            #endregion

            await _invoiceRepository.AddInvoicePaymentsAsync(invoicePayments);
            await _unitOfWork.SaveAsync(cancellationToken);
            var response = new AddInvoiceResponse(invoice.Id);
            return response;

        }
    }
}
