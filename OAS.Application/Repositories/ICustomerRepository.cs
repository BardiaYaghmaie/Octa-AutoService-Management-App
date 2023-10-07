using OAS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Repositories
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer entity);
        Task GetAllAsync();
        void Delete(Customer entity);
        void Update(Customer entity);
        Task<int> GetNewVehicleCode();
        Task<int> GetNewCustomerCode();
    }
}
