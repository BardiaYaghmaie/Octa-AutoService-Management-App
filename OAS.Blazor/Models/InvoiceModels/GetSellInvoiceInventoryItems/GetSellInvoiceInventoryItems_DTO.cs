namespace OAS.Blazor.Models.InvoiceModels.GetSellInvoiceInventoryItems;

public sealed record GetSellInvoiceInventoryItems_DTO(Guid InventoryItemId,Guid InvoiceInventoryItemId,string InventoryItemCode,int RowNumber, string InventoryItemName, float InventoryItemCount, long UnitBuyPrice,long UnitSellPrice, float TotalPrice);
