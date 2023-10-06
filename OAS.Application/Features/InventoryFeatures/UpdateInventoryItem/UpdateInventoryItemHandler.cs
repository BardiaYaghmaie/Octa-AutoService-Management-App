using AutoMapper;
using MediatR;
using OAS.Application.Features.InventoryFeatures.UpdateService;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.UpdateInventoryItem
{
    public class UpdateInventoryItemHandler : IRequestHandler<UpdateInventoryItemRequest, UpdateInventoryItemResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInventoryItemRepository _inventoryItemRepository;
        private readonly IInventoryItemHistoryRepository _inventoryItemHistoryRepository;

        public UpdateInventoryItemHandler(IMapper mapper, IUnitOfWork unitOfWork, IInventoryItemRepository inventoryItemRepository, IInventoryItemHistoryRepository inventoryItemHistoryRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _inventoryItemRepository = inventoryItemRepository;
            _inventoryItemHistoryRepository = inventoryItemHistoryRepository;
        }

        public async Task<UpdateInventoryItemResponse> Handle(UpdateInventoryItemRequest request, CancellationToken cancellationToken)
        {
            InventoryItem? inventoryItem = await _inventoryItemRepository.GetByIdAsync(request.Id);
            if (inventoryItem == null)
                throw new Exception("");
            var inventoryItemNew = _mapper.Map<InventoryItem>(request);
            _inventoryItemRepository.Update(inventoryItemNew);
            var inventoryItemHistory = _mapper.Map<InventoryItemHistory>(inventoryItemNew);
            await _inventoryItemHistoryRepository.AddAsync(inventoryItemHistory);
            await _unitOfWork.SaveAsync(cancellationToken);

            var response = new UpdateInventoryItemResponse(inventoryItem.Id);
            return response;
        }
    }
}
