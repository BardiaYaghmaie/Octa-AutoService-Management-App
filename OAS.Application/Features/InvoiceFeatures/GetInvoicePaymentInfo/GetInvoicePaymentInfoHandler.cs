using MediatR;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetInvoicePaymentInfo
{
    public sealed class GetInvoicePaymentInfoHandler : IRequestHandler<GetInvoicePaymentInfoRequest, GetInvoicePaymentInfoResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;
        private readonly IServiceHistoryRepository _serviceHistoryRepository;

        public GetInvoicePaymentInfoHandler(IInvoiceRepository invoiceRepository, ICustomerRepository customerRepository, IVehicleRepository vehicleRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository, IServiceHistoryRepository serviceHistoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
            _serviceHistoryRepository = serviceHistoryRepository;
        }

        public async Task<GetInvoicePaymentInfoResponse> Handle(GetInvoicePaymentInfoRequest request, CancellationToken cancellationToken)
        {
            Customer? customer = null;
            Vehicle? vehicle = null;
            float total = 0;
            bool invoiceUseBuyPrice = false;
            var invoice = await _invoiceRepository.GetById(request.InvoiceId);
            var response = new GetInvoicePaymentInfoResponse();
            invoiceUseBuyPrice = invoice.UseBuyPrice.HasValue ? invoice.UseBuyPrice.Value : false;
            response.InvoiceId = invoice.Id;
            response.InvoiceType = invoice.Type;
            response.SellerName = invoice.SerllerName;
            response.InvoiceCode = invoice.Code.ToString();
            if (invoice == null) throw new Exception("");
            if (invoice.CustomerId.HasValue)
            {
                customer = await _customerRepository.GetByIdAsync(invoice.CustomerId.Value);
                if (customer == null) throw new Exception("");
                response.CustomerId = customer.Id;
                response.CustomerName = customer.FirstName + " " + customer.LastName;
            }
            else
            {
                response.CustomerName = "";
            }

            if (invoice.VehicleId.HasValue)
            {
                vehicle = await _vehicleRepository.GetByIdAsync(invoice.VehicleId.Value);
                if (vehicle == null) throw new Exception("");
                response.VehicleId = vehicle.Id;
                response.VehicleName = vehicle.Name;
                response.VehiclePlate = vehicle.Plate;
                response.VehicleColor = vehicle.Color;
            }
            else
            {
                response.VehicleName = "";
                response.VehiclePlate = "";
                response.VehicleColor = "";

            }

            var invoicePayments = await _invoiceRepository.GetInvoicePaymentsByInvoiceIdAsync(request.InvoiceId);
            var totalPaid = invoicePayments.Sum(a => a.PaidAmount);
            response.PaidAmoutSoFar = totalPaid;
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
            response.Total = total;
            response.PaymentHistoryDTOs = invoicePayments.Select(a => new GetInvoicePaymentInfo_InvoicePaymentHistoryDTO()
            {
                InvoiceId = a.InvoiceId,
                InvoicePaymentId = a.Id,
                TrackCode = a.TrackCode,
                PaidAmount = a.PaidAmount,
                PaidDate = a.LastPaymentDate
            }).ToList();
            return response;
        }
    }
}
