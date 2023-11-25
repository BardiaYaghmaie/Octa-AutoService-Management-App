namespace OAS.Blazor.Models.InvoiceModels.CreateBuyInvoice;
public sealed record CreateBuyInvoiceRequest(List<CreateBuyInvoice_InventoryItemDTO> Dtos , int Code,string SellerName , DateTime RegisterDate);

