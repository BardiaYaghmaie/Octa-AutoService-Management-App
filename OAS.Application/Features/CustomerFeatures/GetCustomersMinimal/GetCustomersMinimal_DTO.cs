using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.CustomerFeatures.GetCustomersMinimal
{
    public sealed record GetCustomersMinimal_DTO(Guid Id , int Code , string Name);    
}
