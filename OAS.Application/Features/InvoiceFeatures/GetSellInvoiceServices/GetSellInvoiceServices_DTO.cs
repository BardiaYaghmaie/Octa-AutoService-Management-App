using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetSellInvoiceServices
{
    public sealed record GetSellInvoiceServices_DTO(int RowNumber,string ServiceName,int Count,long UnitPrice , long TotalPrice);    
}
