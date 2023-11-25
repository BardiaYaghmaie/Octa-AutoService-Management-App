namespace OAS.Blazor.Models.InvoiceModels.AddSellInvoicePayment;

public sealed record AddInvoicePaymentRequest(Guid InvoiceId , List<Tuple<string ,long>> TrackCodeAndAmountList );
