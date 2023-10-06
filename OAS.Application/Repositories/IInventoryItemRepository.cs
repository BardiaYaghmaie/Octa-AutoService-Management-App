using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Repositories
{
    public interface IInventoryItemRepository
    {
        Task<List<InventoryItem>> GetAllAsync();
        Task AddAsync(InventoryItem entity);
        void Update(InventoryItem entity);
        void Delete(InventoryItem entity);

    }
}
