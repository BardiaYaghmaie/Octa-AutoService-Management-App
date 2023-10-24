using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.CustomerFeatures.GetCustomers
{
    public record GetCustomersResponse
    {
        public int Count { get; set; }
        public List<GetCustomersResponse_DTO> Data { get; set; }
    }
}
