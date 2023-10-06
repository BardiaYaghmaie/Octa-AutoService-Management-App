using OAS.Application.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.Inventory.GetInventoryItems
{
    public sealed record GetInventoryItemsResponse(List<InventoryItemDTO> InventoryItemDTOs);
}
