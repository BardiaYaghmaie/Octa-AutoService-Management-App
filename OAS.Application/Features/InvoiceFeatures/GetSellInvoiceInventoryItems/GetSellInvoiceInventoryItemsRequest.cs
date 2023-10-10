﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems
{
    public sealed record GetSellInvoiceInventoryItemsRequest(Guid InvoiceId) :IRequest<GetSellInvoiceInventoryItemsResponse>;   
}