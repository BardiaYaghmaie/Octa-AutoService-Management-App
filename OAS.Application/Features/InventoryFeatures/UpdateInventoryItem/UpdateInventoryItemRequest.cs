using MediatR;
using OAS.Application.Features.InventoryFeatures.UpdateService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.UpdateInventoryItem
{
    public sealed record UpdateInventoryItemRequest(Guid Id, string Name, long  BuyPrice , long SellPrice,float Count , float CountLowerBound) : IRequest<UpdateInventoryItemResponse>;
}
