using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Blazor.Models.InvoiceModels.CreateBuyInvoice;

public sealed record CreateBuyInvoice_InventoryItemDTO(Guid Id , long BuyPrice , long SellPrice , float Count , float LowerBoundCount);    
