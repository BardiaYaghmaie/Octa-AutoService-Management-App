using Microsoft.EntityFrameworkCore;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using OAS.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Persistence.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvoiceRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        

        public async Task AddAsync(Invoice entity)
        {
             await _dbContext.Invoices.AddAsync(entity);
        }

        public async Task AddInvoiceInventoryItemsAsync(List<InvoiceInventoryItem> invoiceInventoryItems)
        {
            await _dbContext.InvoiceInventoryItems.AddRangeAsync(invoiceInventoryItems);
        }

        public async Task AddInvoicePaymentsAsync(List<InvoicePayment> invoicePayments)
        {
            await _dbContext.InvoicePayments.AddRangeAsync(invoicePayments);
        }

        public async Task AddInvoiceServicesAsync(List<InvoiceService> invoiceServices)
        {
            await _dbContext.InvoiceServices.AddRangeAsync(invoiceServices);
        }

        public void Delete(Invoice entity)
        {
           _dbContext.Invoices.Remove(entity);
        }

        public async Task DeleteInvoiceInventoryItemsAsync(List<Guid> invoiceInventoryItemIds)
        {
            var entities = await _dbContext.InvoiceInventoryItems.Where(a=> invoiceInventoryItemIds.Contains(a.Id)).ToListAsync();
            foreach (var item in entities)
            {
                _dbContext.InvoiceInventoryItems.Remove(item);
            }
        }

        public async Task DeleteInvoiceServicesAsync(List<Guid> invoiceServicesIds)
        {
            var entities = await _dbContext.InvoiceServices.Where(a => invoiceServicesIds.Contains(a.Id)).ToListAsync();
            foreach (var item in entities)
            {
                _dbContext.InvoiceServices.Remove(item);
            }
        }

        public async Task<Invoice?> GetById(Guid id)
        {
            return await _dbContext.Invoices.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<int> GetNewInvoiceCode()
        {
            return await _dbContext.Invoices.Select(a => a.Code).MaxAsync() + 1;

        }

        public void Update(Invoice entity)
        {
            _dbContext.Invoices.Update(entity);
        }
    }
}
