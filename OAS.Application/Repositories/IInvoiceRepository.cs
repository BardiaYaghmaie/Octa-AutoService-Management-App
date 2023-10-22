﻿using OAS.Application.Features.InvoiceFeatures.GetBuyInvoices;
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
        Task<List<Invoice>> GetAllAsync();
        Task<List<InvoicePayment>> GetAllInvoicePaymentsAsync();
        Task<List<InvoiceInventoryItem>> GetAllInvoiceInventoryItemsAsync();
        Task<InvoiceInventoryItem?> GetInvoiceInventoryItemById(Guid invoiceInventoryItemId);

        Task<GetInvoiceReportInfoResponse> GetInvoiceReportInfoAsync(Guid invoiceId);
        Task<List<Invoice>> GetDailySellInvoicesAsync();
        Task<List<GetSellInvoiceServices_DTO>> GetSellInvoicesServicesAsync(Guid invoiceId);
        Task<List<InvoiceInventoryItem>> GetSellInvoiceInventoryItemsAsync(Guid invoiceId);
        Task<List<Invoice>> GetSellInvoicesAsync(); 
        //Task<List<GetBuyInvoices_InvoiceDTO>> GetBuyInvoicesAsync(); 
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
        Task<List<InvoicePayment>> GetInvoicePaymentsByInvoiceIdAsync(Guid invoiceId);
        Task<List<InvoiceService>> GetInvoiceServicesByInvoiceIdAsync(Guid invoiceId);
        Task<List<InvoiceInventoryItem>> GetInvoiceInventoryItemsByInvoiceId(Guid invoiceId);
    }
}
