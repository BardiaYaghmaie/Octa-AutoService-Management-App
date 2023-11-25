using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS.Blazor.Models.CustomerModels.GetCustomers;


public record GetCustomersResponse_DTO
{
    public int RowNumber{ get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Code { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public string FullName => FirstName + " " + LastName;

}
