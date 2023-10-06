﻿using MediatR;
using OAS.Application.Features.InventoryFeatures.AddService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.UpdateService
{
    public sealed record UpdateServiceRequest(Guid Id,string Name, long DefaultPrice) : IRequest<UpdateServiceResponse>;
}