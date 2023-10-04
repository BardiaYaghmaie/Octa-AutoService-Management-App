namespace OAS.Domain.Models;

public class Invoice
{
    public Guid Id { get; set; }
    public Guid VehicleId { get; set; }
    public long DiscountAmount{ get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime RegisterDate { get; set; }
    public virtual ICollection<InvoiceDescription>  InvoiceDescriptions { get; set; }
    public virtual ICollection<InvoiceInventoryItem> InvoiceInventoryItems{ get; set; }
    public virtual ICollection<InvoicePayment>  InvoicePayments{ get; set; }
    public virtual ICollection<InvoiceService> InvoiceServiceItems{ get; set; }
    public virtual Vehicle Vehicle { get; set; }


}
