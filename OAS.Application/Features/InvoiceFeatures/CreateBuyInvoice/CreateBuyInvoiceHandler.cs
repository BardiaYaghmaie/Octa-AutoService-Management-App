using AutoMapper;
using MediatR;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.CreateBuyInvoice
{
    public sealed class CreateBuyInvoiceHandler : IRequestHandler<CreateBuyInvoiceRequest, CreateBuyInvoiceResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CreateBuyInvoiceHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork, IMapper mapper, IInventoryItemRepository inventoryItemRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _inventoryItemRepository = inventoryItemRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
        }


        public async Task<CreateBuyInvoiceResponse> Handle(CreateBuyInvoiceRequest request, CancellationToken cancellationToken)
        {
            var inventoryItems = await _inventoryItemRepository.GetAllAsync();
            foreach (var item in request.Dtos)
            {
                var original = inventoryItems.First(a => a.Id == item.Id);
                if (item.SellPrice == original.SellPrice && item.BuyPrice == original.BuyPrice
                    && item.Count == original.Count && item.LowerBoundCount == original.CountLowerBound
                    )
                {
                    //this means no change happened to the inventory item
                }
                else
                {
                    original.SellPrice = item.SellPrice;
                    original.BuyPrice = item.BuyPrice;
                    original.Count = item.Count; 
                    original.CountLowerBound = item.LowerBoundCount;


                    InventoryItemHistory history = new()
                    {
                        Id = Guid.NewGuid(),
                        BuyPrice = original.SellPrice,
                        Code = original.Code,
                        Count = original.Count,
                        CountLowerBound = original.CountLowerBound,
                        IsActive = original.IsActive,
                        SellPrice = original.SellPrice,
                        UpdateDate = DateTime.Now,
                        InventoryItemId = original.Id
                    };
                    _inventoryItemRepository.Update(original);
                    await _inventoryItemHistoryRepository.AddAsync(history);
                    await _unitOfWork.SaveAsync(cancellationToken);
                }

            }
            var invoiceId = Guid.NewGuid();
            var invoiceInventoryItems = request.Dtos.Select(a=> new InvoiceInventoryItem
            {
                Id = Guid.NewGuid(),
                InventoryItemId = a.Id,
                InvoiceId = invoiceId                
                
            }).ToList();            
            Invoice invoice = new()
            {
                Code = request.Code,
                SerllerName = request.SellerName,
                RegisterDate = request.RegisterDate,
                Type = Domain.Enums.InvoiceType.Buy,
                InvoiceInventoryItems = invoiceInventoryItems,               
            };

            await _invoiceRepository.AddAsync(invoice);
            await _unitOfWork.SaveAsync(cancellationToken);
            var response =  new CreateBuyInvoiceResponse(invoiceId);
            return response;
        }
    }
}
