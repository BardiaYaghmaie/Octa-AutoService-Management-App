﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InventoryFeatures.AddInventoryItem
{
    public sealed record AddInventoryItemRequest(string Name) :IRequest<AddInventoryItemResponse>;    
}
