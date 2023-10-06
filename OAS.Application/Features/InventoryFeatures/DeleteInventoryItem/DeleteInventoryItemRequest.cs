using MediatR;
using OAS.Application.Features.InventoryFeatures.DeleteService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.DeleteInventoryItem
{
    public sealed record DeleteInventoryItemRequest(int Code) : IRequest<DeleteInventoryItemResponse>;

}
