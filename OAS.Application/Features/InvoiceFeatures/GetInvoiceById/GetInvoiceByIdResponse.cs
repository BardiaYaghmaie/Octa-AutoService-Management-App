﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetInvoiceById
{
    public sealed record GetInvoiceByIdResponse(Guid Id  , string Code);    
}