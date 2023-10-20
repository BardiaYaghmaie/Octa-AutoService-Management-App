using OAS.Application.Features.CustomerFeatures.GetCustomersMinimal;
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
        Task<List<GetCustomersMinimal_DTO>> Get();
        Task<Customer?> GetByIdAsync(Guid id);
        Task AddAsync(Customer entity);
        Task<List<Customer>> GetAllAsync();
        void Delete(Customer entity);
        void Update(Customer entity);
        Task<int> GetNewVehicleCode();
        Task<int> GetNewCustomerCode();
    }
}
