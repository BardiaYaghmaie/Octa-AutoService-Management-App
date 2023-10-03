namespace OAS.Domain.Models;

public class Invoice
{
    public Guid Id { get; set; }
    public int VehicleId{ get; set; }
    public long Discount{ get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime RegisterDate { get; set; }
}
