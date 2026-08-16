public class FlightServices : IFlightServices
{
    public Task<ServicesResponse> CreateAsync(CreateFlightRequest dto, Guid airlineStaffId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<FlightListItemResponse>> GetAllAsync(string? origin, string? destination, DateTime? date)
    {
        throw new NotImplementedException();
    }

    public Task<FlightResponse> GetByIdAsync(int flightId)
    {
        throw new NotImplementedException();
    }

    public Task<ServicesResponse> PublishAsync(int flightId, Guid airlineStaffId)
    {
        throw new NotImplementedException();
    }
}