﻿namespace OAS.Blazor.Models.InvoiceModels.GetDailySellInvoices;

public sealed record GetDailySellInvoices_DTO(Guid InvoiceId, string InvoiceCode, string VehicleName, string CustomerName, int RowNumber, float Total);
