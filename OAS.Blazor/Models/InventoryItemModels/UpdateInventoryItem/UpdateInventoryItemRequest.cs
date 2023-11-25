namespace OAS.Blazor.Models.InventoryItemModels.UpdateInventoryItem;
public sealed record UpdateInventoryItemRequest(Guid Id, string Name, long  BuyPrice , long SellPrice,float Count , float CountLowerBound);
