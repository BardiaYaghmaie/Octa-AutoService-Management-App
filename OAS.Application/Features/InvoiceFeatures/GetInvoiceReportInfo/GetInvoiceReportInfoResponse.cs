﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetInvoiceReportInfo
{
    public sealed record GetInvoiceReportInfoResponse(  string InvoiceCode,string VehicleCode , string CustomerTitle , string VehicleTitle,string VehiclePlate , string VehicleColor , DateTime InvoiceDate , float TotalPrice , float Discount , float Tax , float ToPay,List<GetInvoiceReportInfo_ItemDTO> Items);    
}
