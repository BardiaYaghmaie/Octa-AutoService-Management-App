namespace OAS.Blazor.Models.InvoiceModels.GetInvoicePaymentInfo;

public sealed record GetInvoicePaymentInfo_InvoicePaymentHistoryDTO
{
    public Guid InvoicePaymentId { get; set; }
    public Guid InvoiceId { get; set; }
    public string TrackCode { get; set; }
    public long PaidAmount { get; set; }
    public DateTime PaidDate { get; set; }
}
