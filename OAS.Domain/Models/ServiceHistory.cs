namespace OAS.Domain.Models;

public class ServiceHistory
{
    public Guid Id { get; set; }
    public string ServiceId{ get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }
    public virtual Service Service { get; set; }

}


