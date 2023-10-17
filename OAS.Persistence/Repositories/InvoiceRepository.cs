using Microsoft.EntityFrameworkCore;
using OAS.Application.Features.InvoiceFeatures.GetBuyInvoices;
using OAS.Application.Features.InvoiceFeatures.GetDailySellInvoices;
using OAS.Application.Features.InvoiceFeatures.GetInvoiceReportInfo;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoices;
using OAS.Application.Features.InvoiceFeatures.GetSellInvoiceServices;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using OAS.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            var invoices = await _dbContext.Invoices.ToListAsync();
            var inventoryItemHistories = await _dbContext.InventoryItemHistories.ToListAsync();
            var invoicePayments = await _dbContext.InvoicePayments.ToListAsync();
            var invoiceInventoryItems = await _dbContext.InvoiceInventoryItems.ToListAsync();
            var data = invoices.Where(a => a.Type == Domain.Enums.InvoiceType.Buy).Select((a, i) =>
            {
                long paidAmount = invoicePayments.Where(b => b.InvoiceId == a.Id).Select(b => b.PaidAmount).Sum();
                float total = 0;
                foreach (var invoiceInventoryItem in invoiceInventoryItems.Where(b => b.InvoiceId == a.Id).ToList())
                {
                    float count = invoiceInventoryItem.Count;
                    DateTime registerDate = invoiceInventoryItem.RegisterDate;
                    var inventoryItemId = invoiceInventoryItem.InventoryItemId;
                    var buyPrice = inventoryItemHistories.Where(b => b.InventoryItemId == inventoryItemId && b.BuyPrice.HasValue && b.UpdateDate <= registerDate).OrderByDescending(b => b.UpdateDate).Select(b => b.BuyPrice.Value).First();
                    total += (count * buyPrice);
                }

                return new GetBuyInvoices_InvoiceDTO
                (
                    SellerName: a.SerllerName,
                    InvoiceCode: a.Code.ToString(),
                    InvoiceDate: a.RegisterDate,
                    InvoiceDateString: a.RegisterDate.ToString(),
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
            var data1 = data.Where(a => a.VehicleId.HasValue || a.CustomerId.HasValue)
            .Select((a, i) => new GetDailySellInvoices_DTO(
                a.Id,
                a.Code.ToString(),
                a.VehicleId.HasValue ? a.Vehicle.Name : "",
                a.VehicleId.HasValue ? (a.Vehicle.Customer.FirstName + a.Vehicle.Customer.LastName) : a.CustomerId.HasValue ? (a.Customer.FirstName + " " + a.Customer.LastName) : "",
                i + 1

            )).ToList();
            return data1;
        }


        public async Task<InvoiceInventoryItem?> GetInvoiceInventoryItemById(Guid invoiceInventoryItemId)
        {
            return await  _dbContext.InvoiceInventoryItems.FirstOrDefaultAsync(a => a.Id == invoiceInventoryItemId);
        }


        public async Task<List<InvoiceInventoryItem>> GetInvoiceInventoryItemsByInvoiceId(Guid invoiceId)
        {
            return await _dbContext.InvoiceInventoryItems.Where(a => a.InvoiceId == invoiceId).ToListAsync();
        }

        public async Task<GetInvoiceReportInfoResponse> GetInvoiceReportInfoAsync(Guid invoiceId)
        {
            var invoice = await _dbContext.Invoices.Include(a => a.Vehicle).ThenInclude(a => a.Customer).Include(a => a.Customer).AsNoTracking().FirstAsync(a => a.Id == invoiceId);
            string invoiceCode = invoice.Code.ToString();
            DateTime invoiceDate = invoice.RegisterDate;
            string customerName = invoice.VehicleId.HasValue ? (invoice.Vehicle.Customer.FirstName + " " + invoice.Vehicle.Customer.LastName) : (invoice.CustomerId.HasValue ? invoice.Customer.FirstName + " " + invoice.Customer.LastName : throw new Exception("invoice does not have customer"));
            string customerCode = invoice.CustomerId.HasValue ? invoice.Customer.Code.ToString() : "";
            string vehicleName = invoice.VehicleId.HasValue ? (invoice.Vehicle.Name) : ("");
            string vehiclePlate = invoice.VehicleId.HasValue ? (invoice.Vehicle.Plate) : ("");
            string vehicleColor = invoice.VehicleId.HasValue ? (invoice.Vehicle.Color) : ("");
            string vehicleCode = invoice.VehicleId.HasValue ? (invoice.Vehicle.Code.ToString()) : ("");
            var invoiceInventoryItems = await _dbContext.InvoiceInventoryItems.Include(a => a.InventoryItem).AsNoTracking().Where(a => a.InvoiceId == invoiceId).ToListAsync();
            var invoiceServices = await _dbContext.InvoiceServices.Include(a => a.Service).AsNoTracking().Where(a => a.InvoiceId == invoiceId).ToListAsync();
            List<GetInvoiceReportInfo_ItemDTO> items = new();
            int rowNumber = 1;
            float invoiceTotal = 0;
            float invoiceTax = 0;
            float invoiceDiscount = invoice.DiscountAmount.HasValue ? invoice.DiscountAmount.Value : 0;
            foreach (var item in invoiceInventoryItems)
            {
                float unitPrice = invoice.UseBuyPrice.HasValue && invoice.UseBuyPrice.Value ? (item.InventoryItem.BuyPrice.Value) : (item.InventoryItem.SellPrice.Value);
                items.Add(new GetInvoiceReportInfo_ItemDTO(rowNumber.ToString(), item.InventoryItem.Name, item.Count.ToString(), unitPrice.ToString(), (item.Count * unitPrice).ToString()));
                rowNumber++;
                invoiceTotal += item.Count * unitPrice;
            }
            foreach (var item in invoiceServices)
            {
                float unitPrice = item.Price;
                items.Add(new GetInvoiceReportInfo_ItemDTO(rowNumber.ToString(), item.Service.Name, "1", unitPrice.ToString(), (1 * unitPrice).ToString()));
                rowNumber++;
                invoiceTotal += 1 * unitPrice;
            }
            var answer = new GetInvoiceReportInfoResponse(invoiceCode, vehicleCode, customerName, vehicleName, vehiclePlate, vehicleColor, invoiceDate, invoiceTotal.ToString(), invoiceDiscount.ToString(), invoiceTax.ToString(), (invoiceTotal - invoiceTax - invoiceDiscount).ToString(), items);
            return answer;

        }

        public async Task<int> GetNewInvoiceCode()
        {
            if (await _dbContext.Invoices.CountAsync() == 0) return 1;
            return await _dbContext.Invoices.Select(a => a.Code).MaxAsync() + 1;

        }

        public async Task<List<GetSellInvoiceInventoryItems_DTO>> GetSellInvoiceInventoryItemsAsync(Guid invoiceId)
        {
            var data = await _dbContext.InvoiceInventoryItems.Include(a => a.InventoryItem).Where(a => a.InvoiceId == invoiceId).ToListAsync();
            var answer = data.Select((a, i) => new GetSellInvoiceInventoryItems_DTO
            (
                RowNumber: i + 1,
                InventoryItemCode: a.InventoryItem.Code.ToString(),
                InvoiceInventoryItemId: a.Id,
                InventoryItemId: a.InventoryItemId,
                InventoryItemName: a.InventoryItem.Name,
                InventoryItemCount: a.InventoryItem.Count.Value, //todo
                UnitBuyPrice: a.InventoryItem.BuyPrice.Value,
                UnitSellPrice: a.InventoryItem.SellPrice.Value,
                TotalPrice: a.InventoryItem.Count.Value * a.InventoryItem.SellPrice.Value
            )).ToList();
            return answer;
        }

        public async Task<List<GetSellInvoices_InvoiceDTO>> GetSellInvoicesAsync()
        {
            var invoices = await _dbContext.Invoices.Include(a => a.Vehicle).ThenInclude(a => a.Customer).Include(a => a.Customer).ToListAsync();
            var inventoryItemHistories = await _dbContext.InventoryItemHistories.ToListAsync();
            var invoicePayments = await _dbContext.InvoicePayments.ToListAsync();
            var invoiceInventoryItems = await _dbContext.InvoiceInventoryItems.ToListAsync();
            var data = invoices.Where(a => a.Type == Domain.Enums.InvoiceType.Sell).Select((a, i) =>
            {
                long paidAmount = invoicePayments.Where(b => b.InvoiceId == a.Id).Select(b => b.PaidAmount).Sum();
                float total = 0;
                foreach (var invoiceInventoryItem in invoiceInventoryItems.Where(b => b.InvoiceId == a.Id).ToList())
                {
                    float count = invoiceInventoryItem.Count;
                    DateTime registerDate = invoiceInventoryItem.RegisterDate;
                    var inventoryItemId = invoiceInventoryItem.InventoryItemId;
                    var buyPrice = inventoryItemHistories.Where(b => b.InventoryItemId == inventoryItemId && b.BuyPrice.HasValue && b.UpdateDate <= registerDate).OrderByDescending(b => b.UpdateDate).Select(b => a.UseBuyPrice.HasValue && a.UseBuyPrice.Value ? b.BuyPrice : b.SellPrice.Value).FirstOrDefault() ?? 0;
                    total += (count * buyPrice);
                }

                return new GetSellInvoices_InvoiceDTO
                (
                    InvoiceCode: a.Code.ToString(),
                    InvoiceDate: a.RegisterDate,
                    InvoiceDateString: a.RegisterDate.ToString(),
                    InvoiceId: a.Id,
                    RowNumber: i + 1,
                    InvoiceTotalPrice: total,
                    InvoicePaidAmount: paidAmount,
                    VehicleName: a.Vehicle?.Name,
                    CustomerName: a.VehicleId.HasValue ? (a.Vehicle.Customer.FirstName + " " + a.Vehicle.Customer.LastName) : (a.CustomerId.HasValue ? (a.Customer.FirstName + " " + a.Customer.LastName) : (""))

                );
            }).ToList();
            return data;
        }

        public async Task<List<GetSellInvoiceServices_DTO>> GetSellInvoicesServicesAsync(Guid invoiceId)
        {
            var data = await _dbContext.InvoiceServices.Include(a => a.Service).Where(a => a.InvoiceId == invoiceId).ToListAsync();
            var answer = data.Select((a, i) => new GetSellInvoiceServices_DTO
            (
                Code: a.Service.Code.ToString(),
                ServiceId: a.ServiceId,
                InvoiceServiceId: a.Id,
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
