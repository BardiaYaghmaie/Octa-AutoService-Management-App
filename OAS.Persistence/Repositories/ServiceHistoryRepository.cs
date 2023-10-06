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
    public class ServiceHistoryRepository : IServiceHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(ServiceHistory entity)
        {
            await _context.ServiceHistories.AddAsync(entity);
        }
    }
}
