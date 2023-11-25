namespace OAS.Blazor.Models.CustomerModels.AddCustomer;

public sealed record AddCustomerRequest(string FirstName , string LastName , string phoneNumber,
    DateTime RegisterDate , List<VehicleDTO> VehicleDTOs);   
