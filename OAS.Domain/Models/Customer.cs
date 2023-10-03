﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Domain.Models;
public class Customer
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }
}
