﻿namespace OAS.Domain.Models;

public class Service
{
    public Guid Id { get; set; }
    public string Name{ get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }
    public virtual ICollection<InvoiceService>  InvoiceServices{ get; set; }
    public virtual ICollection<ServiceHistory> ServiceHistories{ get; set; }



}


