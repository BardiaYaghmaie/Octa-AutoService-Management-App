﻿using OAS.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OAS.Application.DomainModels;

namespace OAS.Application.Features.Inventory.GetInventoryItems
{
    public sealed class GetInventoryItemsHandler : IRequestHandler<GetInventoryItemsRequest, GetInventoryItemsResponse>
    {
        private readonly IInventoryItemRepository _inventoryItemRepository;

        public GetInventoryItemsHandler(IInventoryItemRepository inventoryItemRepository)
        {
            _inventoryItemRepository = inventoryItemRepository;
        }

        public async Task<GetInventoryItemsResponse> Handle(GetInventoryItemsRequest request, CancellationToken cancellationToken)
        {
            var inventories = await _inventoryItemRepository.GetAllAsync();
            var inventoryItemDTOs=inventories.Select((item, index) =>
            {
                return new InventoryItemDTO(RowNumber: index + 1,
                    Code: item.Code.ToString(),
                    Title: item.Name,
                    Count: item.Count,
                    Limit: item.CountLowerBound,
                    BuyPrice: item.BuyPrice,
                    SellPrice: item.SellPrice);
            }).ToList();
            var response = new GetInventoryItemsResponse(inventoryItemDTOs);
            return response;
        }
    }
}
