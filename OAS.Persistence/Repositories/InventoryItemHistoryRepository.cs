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
    public class InventoryItemHistoryRepository : IInventoryItemHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryItemHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(InventoryItemHistory entity)
        {
            await _context.InventoryItemHistories.AddAsync(entity);
        }

        public async Task<InventoryItemHistory?> GetLatestByInventoryItemIdAndDateAsync(Guid inventoryItemId, DateTime dateTime)
        {
            var data = await _context.InventoryItemHistories.Where(a => a.InventoryItemId == inventoryItemId && a.UpdateDate <= dateTime).OrderByDescending(a => a.UpdateDate).FirstOrDefaultAsync();
            return data;
        }
    }
}
