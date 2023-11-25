namespace OAS.Blazor.Models.InvoiceModels.GetInvoiceReportInfo;

public sealed record GetInvoiceReportInfoResponse(  string InvoiceCode,string VehicleCode , string CustomerTitle , string VehicleTitle,string VehiclePlate , string VehicleColor , DateTime InvoiceDate , float TotalPrice , float Discount , float Tax , float ToPay,string Description,List<GetInvoiceReportInfo_ItemDTO> Items);    
