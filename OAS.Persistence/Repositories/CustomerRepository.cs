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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Customer entity)
        {
            await _dbContext.Customers.AddAsync(entity);
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNewCustomerCode()
        {
            return await _dbContext.Customers.Select(a => a.Code).MaxAsync() + 1;
        }

        public async Task<int> GetNewVehicleCode()
        {
            return await _dbContext.Vehicles.Select(a => a.Code).MaxAsync() + 1;

        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
