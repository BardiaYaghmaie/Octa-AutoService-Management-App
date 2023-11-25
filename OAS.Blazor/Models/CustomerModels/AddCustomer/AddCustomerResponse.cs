namespace OAS.Blazor.Models.CustomerModels.AddCustomer;

public sealed record AddCustomerResponse(Guid CustomerId , List<VehicleDTO> VehicleDTOs);    
