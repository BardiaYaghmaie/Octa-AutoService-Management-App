namespace OAS.Blazor.Models.InventoryItemModels.UpdateService;

public sealed record UpdateServiceRequest(Guid Id,string Name, long DefaultPrice);
