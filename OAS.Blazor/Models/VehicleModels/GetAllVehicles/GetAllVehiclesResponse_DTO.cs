﻿namespace OAS.Blazor.Models.VehicleModels.GetAllVehicles;

public record GetAllVehiclesResponse_DTO
{
    public int RowNumber { get; set; }
    public Guid VehicleId{ get; set; }
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string VehicleCode { get; set; }
    public string CustomerCode { get; set; }
    public string VehicleName { get; set; }
    public string FullName => FirstName + " " + LastName;
}
