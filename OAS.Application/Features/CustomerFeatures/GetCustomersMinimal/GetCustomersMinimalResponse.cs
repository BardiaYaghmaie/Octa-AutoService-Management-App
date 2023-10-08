using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.CustomerFeatures.GetCustomersMinimal
{
    public sealed record GetCustomersMinimalResponse(List<GetCustomersMinimal_DTO> Data);    
}
