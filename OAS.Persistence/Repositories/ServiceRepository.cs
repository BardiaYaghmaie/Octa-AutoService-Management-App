﻿using System;
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

        public void Delete(Service entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetByCode(int code)
        {
            return await _context.Services.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<Service?> GetByIdAsync(Guid id)
        {
            return await _context.Services.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<int> GetNewCodeAsync()
        {
            return await _context.Services.Select(a=> a.Code).MaxAsync();
        }

        public void Update(Service entity)
        {
            throw new NotImplementedException();
        }
    }
}