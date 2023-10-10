using OAS.Application.Features.InvoiceFeatures.GetSellInvoices;

namespace OAS.Application.Features.InvoiceFeatures.GetDailySellInvoices
{
    public sealed record GetDailySellInvoicesResponse(List<GetDailySellInvoices_DTO> Data);
}