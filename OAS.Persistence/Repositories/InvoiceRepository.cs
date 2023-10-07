using OAS.Application.Repositories;
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
        

        public Task CreateSellInvoice(Guid VehicleId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNewInvoiceCode()
        {
            throw new NotImplementedException();

        }
    }
}
