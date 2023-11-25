using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Blazor.Models.CustomerModels.GetCustomersMinimal;

public sealed record GetCustomersMinimal_DTO(Guid Id , int Code , string Name);    
