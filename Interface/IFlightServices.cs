public interface IFlightServices
{
    Task<ServicesResponse<int>> CreateAsync(CreateFlightRequest dto, Guid airlineStaffId);
    Task<ServicesResponse> PublishAsync(int flightId, Guid airlineStaffId);
    Task<IEnumerable<FlightListItemResponse>> GetAllAsync(string? origin, string? destination, DateTime? date);
    Task<FlightResponse> GetByIdAsync(int flightId);
}
