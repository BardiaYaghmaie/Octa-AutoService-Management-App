using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Repositories
{
    public interface IInventoryItemHistoryRepository
    {
        Task AddAsync(InventoryItemHistory entity);
        Task<InventoryItemHistory?> GetLatestByInventoryItemIdAndDateAsync(Guid inventoryItemId, DateTime dateTime);        
    }
}
