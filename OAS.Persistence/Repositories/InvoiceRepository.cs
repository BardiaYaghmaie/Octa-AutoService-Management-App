using Microsoft.EntityFrameworkCore;
using OAS.Application.Features.InvoiceFeatures.GetBuyInvoices;
using OAS.Application.Features.InvoiceFeatures.GetDailySellInvoices;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoices;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
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
            var entities = await _dbContext.InvoiceInventoryItems.Where(a => invoiceInventoryItemIds.Contains(a.Id)).ToListAsync();
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

        public async Task<List<GetBuyInvoices_InvoiceDTO>> GetBuyInvoicesAsync()
        {
            var d = await _dbContext.Invoices.Include(a => a.Customer).Include(a => a.InvoiceInventoryItems).Include(a => a.InvoicePayments).Where(a => a.Type == Domain.Enums.InvoiceType.Buy).ToListAsync();
            var data = d.Select((a, i) =>
            {
                long paidAmount = a.InvoicePayments.Select(b => b.PaidAmount).Sum();
                long total = a.InvoiceServices.Select(b => b.Price).Sum() +
                +a.InvoiceInventoryItems.Select(b => b.InventoryItem).Select(b => b.BuyPrice.Value).Sum();

                return new GetBuyInvoices_InvoiceDTO
                (
                    CustomerName: "",
                    InvoiceCode: a.Code.ToString(),
                    InvoiceDate: a.UpdateDate.Value,
                    InvoiceDateString: a.UpdateDate.ToString(),
                    InvoiceId: a.Id,
                    RowNumber: i + 1,
                    InvoiceTotalPrice: total,
                    InvoicePaidAmount: paidAmount

                );
            }).ToList();
            return data;
        }

        public async Task<Invoice?> GetById(Guid id)
        {
            return await _dbContext.Invoices.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<GetDailySellInvoices_DTO>> GetDailySellInvoicesAsync()
        {
            var data = await _dbContext.Invoices
                .Include(a => a.Customer).Include(a => a.Vehicle).ThenInclude(a => a.Customer).Include(a => a.InvoiceInventoryItems).Include(a => a.InvoicePayments).Include(a => a.InvoiceServices).Where(a => a.Type == Domain.Enums.InvoiceType.Sell)
                .Where(a => a.RegisterDate.Date == DateTime.Now.Date)
                .ToListAsync();
                var data1=data.Where(a=> a.VehicleId.HasValue || a.CustomerId.HasValue)
                .Select((a,i) => new GetDailySellInvoices_DTO(
                    a.Id,
                    a.Code.ToString(),
                    a.VehicleId.HasValue?a.Vehicle.Name:"",
                    a.CustomerId.HasValue?a.Customer.FirstName + " " + a.Customer.LastName:"",
                    i+1

                )).ToList();
            return data1;
        }

        public async Task<int> GetNewInvoiceCode()
        {
            return await _dbContext.Invoices.Select(a => a.Code).MaxAsync() + 1;

        }

        public async Task<List<GetSellInvoiceInventoryItems_DTO>> GetSellInvoiceInventoryItemsAsync(Guid invoiceId)
        {
            var data = await _dbContext.InvoiceInventoryItems.Include(a => a.InventoryItem).Where(a => a.InvoiceId == invoiceId).ToListAsync();
            var answer = data.Select((a, i) => new GetSellInvoiceInventoryItems_DTO
            (
                RowNumber: i + 1,
                InventoryItemName: a.InventoryItem.Name,
                InventoryItemCount: a.InventoryItem.Count.Value, //todo
                UnitPrice: a.InventoryItem.SellPrice.Value,
                TotalPrice: a.InventoryItem.Count.Value * a.InventoryItem.SellPrice.Value
            )).ToList();
            return answer;
        }

        public async Task<List<GetSellInvoices_InvoiceDTO>> GetSellInvoicesAsync()
        {
            var d = await _dbContext.Invoices.Include(a => a.Customer).Include(a => a.Vehicle).ThenInclude(a => a.Customer).Include(a => a.InvoiceInventoryItems).Include(a => a.InvoicePayments).Include(a => a.InvoiceServices).Where(a => a.Type == Domain.Enums.InvoiceType.Sell).ToListAsync();
            var data = d.Select((a, i) =>
                {
                    long paidAmount = a.InvoicePayments.Select(b => b.PaidAmount).Sum();
                    long total = a.InvoiceServices.Select(b => b.Price).Sum() +
                    +a.InvoiceInventoryItems.Select(b => b.InventoryItem).Select(b => a.UseBuyPrice.Value ? b.BuyPrice.Value : b.SellPrice.Value).Sum();

                    return new GetSellInvoices_InvoiceDTO
                    (
                        CustomerName: a.VehicleId == null ? a.Customer?.FirstName + " " + a.Customer?.LastName : a.Vehicle.Customer.FirstName + " " + a.Vehicle.Customer.LastName,
                        InvoiceCode: a.Code.ToString(),
                        InvoiceDate: a.UpdateDate.Value,
                        InvoiceDateString: a.UpdateDate.ToString(),
                        InvoiceId: a.Id,
                        RowNumber: i + 1,
                        VehicleName: a.Vehicle?.Name,
                        InvoiceTotalPrice: total,
                        InvoicePaidAmount: paidAmount

                    );
                }).ToList();
            return data;
        }

        public async Task<List<GetSellInvoiceServices_DTO>> GetSellInvoicesServicesAsync(Guid invoiceId)
        {
            var data = await _dbContext.InvoiceServices.Include(a => a.Service).Where(a => a.InvoiceId == invoiceId).ToListAsync();
            var answer = data.Select((a, i) => new GetSellInvoiceServices_DTO
            (
                RowNumber: i + 1,
                ServiceName: a.Service.Name,
                Count: 1, //todo
                UnitPrice: a.Price,
                TotalPrice: a.Price
            )).ToList();
            return answer;

        }

        public void Update(Invoice entity)
        {
            _dbContext.Invoices.Update(entity);
        }
    }
}
