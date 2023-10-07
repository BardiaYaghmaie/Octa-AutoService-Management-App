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

        public void Delete(Invoice entity)
        {
           _dbContext.Invoices.Remove(entity);
        }

        public async Task<Invoice?> GetById(Guid id)
        {
            return await _dbContext.Invoices.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<int> GetNewInvoiceCode()
        {
            return await _dbContext.Invoices.Select(a => a.Code).MaxAsync() + 1;

        }
    }
}
