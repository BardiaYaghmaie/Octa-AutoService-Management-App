using System.ComponentModel.DataAnnotations;

namespace OAS.Domain.Models;

public class InvoiceInventoryItem
{
    public Guid Id { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid InventoryItemId { get; set; }
    
    public virtual Invoice Invoice { get; set; }
    public virtual InventoryItem InventoryItem { get; set; }

}
