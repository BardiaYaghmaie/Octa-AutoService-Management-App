namespace OAS.Blazor.Models.InvoiceModels.GetInvoiceById;

public sealed record GetInvoiceByIdResponse(Guid Id  , string Code,bool UseBuyPrice , string Description);    
