namespace OAS.Blazor.Models.InvoiceModels.UpdateInvoiceServicesAndInventoryItems;

public sealed record UpdateInvoiceServicesAndInventoryItemsRequest(Guid InvoiceId , List<Tuple<Guid,float>> InventoryItemIdsAndCounts,
    List<Tuple<Guid , long>> ServiceIdsAndPrices,List<Guid> ToRemoveInvoiceInventoryItemIds , List<Guid> ToRemoveInvoiceServiceIds , bool UseBuyPrice , string Description);    
