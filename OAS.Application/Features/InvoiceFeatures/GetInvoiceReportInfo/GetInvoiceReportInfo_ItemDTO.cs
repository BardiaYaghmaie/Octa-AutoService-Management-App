using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetInvoiceReportInfo
{
    public sealed record GetInvoiceReportInfo_ItemDTO(string RowNumber , string Name , string Count , string UnitPrice , string TotalPrice);
}
