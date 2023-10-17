using AutoMapper;
using MediatR;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.UpdateInvoiceServicesAndInventoryItems
{
    public sealed record class UpdateInvoiceServicesAndInventoryItemsHandler :IRequestHandler<UpdateInvoiceServicesAndInventoryItemsRequest, UpdateInvoiceServicesAndInventoryItemsResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateInvoiceServicesAndInventoryItemsHandler(IMapper mapper, IUnitOfWork unitOfWork, IInvoiceRepository invoiceRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<UpdateInvoiceServicesAndInventoryItemsResponse> Handle(UpdateInvoiceServicesAndInventoryItemsRequest request, CancellationToken cancellationToken)
        {
            Invoice? invoice = await _invoiceRepository.GetById(request.InvoiceId);
            if (invoice == null && invoice?.Type != Domain.Enums.InvoiceType.Sell)
                throw new Exception("invalid invoice");
            invoice.UseBuyPrice = request.UseBuyPrice;
            invoice.UpdateDate = DateTime.UtcNow;
            List<InvoiceService> invoiceServices = request.ServiceIdsAndPrices.Select(a => new InvoiceService
            {
                Id = Guid.NewGuid(),
                InvoiceId = invoice.Id,
                Price = a.Item2,
                ServiceId = a.Item1

            }).ToList();
            List<InvoiceInventoryItem > invoiceInventoryItems = request.InventoryItemIdsAndCounts.Select(a => new InvoiceInventoryItem
            {
                Id = Guid.NewGuid(),
                InventoryItemId = a.Item1,
                Count = a.Item2,
                InvoiceId = invoice.Id
            }).ToList();

            await _invoiceRepository.DeleteInvoiceInventoryItemsAsync(request.ToRemoveInvoiceInventoryItemIds);
            await _invoiceRepository.DeleteInvoiceServicesAsync(request.ToRemoveInvoiceServiceIds);
            await _invoiceRepository.AddInvoiceInventoryItemsAsync(invoiceInventoryItems);
            await _invoiceRepository.AddInvoiceServicesAsync(invoiceServices);
            _invoiceRepository.Update(invoice);
            await _unitOfWork.SaveAsync(cancellationToken);
            var response = new UpdateInvoiceServicesAndInventoryItemsResponse(invoice.Id);
            return response;


        }
    }
}
