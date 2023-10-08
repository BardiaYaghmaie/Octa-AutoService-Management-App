using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.UpdateInvoiceServicesAndInventoryItems
{
    public sealed record UpdateInvoiceServicesAndInventoryItemsRequest(Guid InvoiceId , List<Guid> InventoryItemIds,
        List<Tuple<Guid , long>> ServiceIdsAndPrices,List<Guid> ToRemoveInvoiceInventoryItemIds , List<Guid> ToRemoveInvoiceServiceIds , bool UseBuyPrice):IRequest<UpdateInvoiceServicesAndInventoryItemsResponse>;    
}
