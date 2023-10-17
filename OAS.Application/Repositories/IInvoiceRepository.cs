using OAS.Application.Features.InvoiceFeatures.GetBuyInvoices;
using OAS.Application.Features.InvoiceFeatures.GetDailySellInvoices;
using OAS.Application.Features.InvoiceFeatures.GetInvoiceReportInfo;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoices;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Repositories
{
    public interface IInvoiceRepository
    {
        Task<InvoiceInventoryItem?> GetInvoiceInventoryItemById(Guid invoiceInventoryItemId);

        Task<GetInvoiceReportInfoResponse> GetInvoiceReportInfoAsync(Guid invoiceId);
        Task<List<GetDailySellInvoices_DTO>> GetDailySellInvoicesAsync();
        Task<List<GetSellInvoiceServices_DTO>> GetSellInvoicesServicesAsync(Guid invoiceId);
        Task<List<GetSellInvoiceInventoryItems_DTO>> GetSellInvoiceInventoryItemsAsync(Guid invoiceId);
        Task<List<GetSellInvoices_InvoiceDTO>> GetSellInvoicesAsync(); 
        Task<List<GetBuyInvoices_InvoiceDTO>> GetBuyInvoicesAsync(); 
        Task AddAsync(Invoice entity);
        void Delete(Invoice entity);
        void Update(Invoice entity);
        Task<int> GetNewInvoiceCode();
        Task<Invoice?> GetById(Guid id);
        Task DeleteInvoiceInventoryItemsAsync(List<Guid> invoiceInventoryItemIds);
        Task DeleteInvoiceServicesAsync(List<Guid> invoiceServicesIds);

        Task AddInvoiceInventoryItemsAsync(List<InvoiceInventoryItem> invoiceInventoryItems);    
        Task AddInvoiceServicesAsync(List<InvoiceService> invoiceServices);
        Task AddInvoicePaymentsAsync(List<InvoicePayment> invoicePayments);

        Task<List<InvoiceInventoryItem>> GetInvoiceInventoryItemsByInvoiceId(Guid invoiceId);
    }
}
