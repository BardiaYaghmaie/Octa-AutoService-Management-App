﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Application.Features.InvoiceFeatures.GetSellInvoiceInventoryItems
{
    public sealed record GetSellInvoiceInventoryItems_DTO(int RowNumber, string InventoryItemName, float InventoryItemCount, long UnitPrice, double TotalPrice);
}
