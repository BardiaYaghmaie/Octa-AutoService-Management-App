namespace OAS.Blazor.Models.VehicleModels.GetAllVehicles;

public sealed record GetAllVehiclesResponse
{
    public int Count { get; set; }
    public List<GetAllVehiclesResponse_DTO> Data { get; set; }
}
