namespace OAS.Blazor.Models.InvoiceModels.UpdateInvoiceServicesAndInventoryItems;

public sealed record UpdateInvoiceServicesAndInventoryItemsRequest(Guid InvoiceId , List<(Guid,float)> InventoryItemIdsAndCounts,
    List<(Guid , long)> ServiceIdsAndPrices,List<Guid> ToRemoveInvoiceInventoryItemIds , List<Guid> ToRemoveInvoiceServiceIds , bool UseBuyPrice , string Description);    
