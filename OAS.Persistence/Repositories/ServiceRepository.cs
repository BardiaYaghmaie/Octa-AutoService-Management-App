using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OAS.Application.Repositories;
using OAS.Domain.Models;
using OAS.Persistence.Contexts;

namespace OAS.Persistence.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Service entity)
        {
            await _context.Services.AddAsync(entity);    
        }

        public void  Delete(Service entity)
        {
            _context.Services.Remove(entity);
        }

        public Task<List<Service>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Service>> GetAllAsync()
        {
           return  await _context.Services.ToListAsync();
        }

        public void Update(Service entity)
        {
            _context.Services.Update(entity);
        }
    }
}
