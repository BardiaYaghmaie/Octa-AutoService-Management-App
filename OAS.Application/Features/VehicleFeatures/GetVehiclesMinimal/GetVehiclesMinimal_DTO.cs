﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.VehicleFeatures.GetVehiclesMinimal
{
    public sealed record GetVehiclesMinimal_DTO(Guid Id , int Code , string Name);    
}