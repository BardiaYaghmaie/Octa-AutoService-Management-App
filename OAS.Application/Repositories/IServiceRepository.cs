using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Repositories
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAll();
        Task AddAsync(Service entity);
        void Update(Service entity);
        void Delete(Service entity);
    }
}
