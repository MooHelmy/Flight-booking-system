using Microsoft.EntityFrameworkCore;

public class FlightServices(IGeneric<Flight> flightInterface, ApplicationDbContext context) : IFlightServices
{

    public async Task<ServicesResponse<int>> CreateAsync(CreateFlightRequest dto, Guid airlineStaffId)
    {
        if (dto.ArrivalAt <= dto.DepartureAt)
        {
            return new ServicesResponse<int>(false, "Arrival time must be after departure time");
        }

        if (dto.SalesStartAt >= dto.DepartureAt)
        {
            return new ServicesResponse<int>(false, "Sales start time must be before departure time");
        }

        var duplicateExists = await context.Flights.AnyAsync(f => f.FlightNumber == dto.FlightNumber);
        if (duplicateExists)
        {
            return new ServicesResponse<int>(false, "Flight number already exists");
        }

        Flight flightEntity = dto.FlightToEntityMapper(airlineStaffId);
        var result = await flightInterface.CreateAsync(flightEntity);

        return result > 0
            ? new ServicesResponse<int>(true, "Flight created successfully", flightEntity.Id)
            : new ServicesResponse<int>(false, "Flight not created");
    }

    public async Task<ServicesResponse> PublishAsync(int flightId, Guid airlineStaffId)
    {
        // Fix #1: GetByIdAsync بترجع T? — لازم نتأكد إنها مش null قبل ما نستخدمها
        var flight = await flightInterface.GetByIdAsync(flightId, f => f.FareClasses);

        if (flight is null)
        {
            return new ServicesResponse(false, "Flight not found");
        }

        if (flight.AirlineStaffId != airlineStaffId)
        {
            return new ServicesResponse(false, "You are not allowed to publish this flight");
        }

        if (flight.Status != FlightStatus.Draft)
        {
            return new ServicesResponse(false, "Only draft flights can be published");
        }

        if (!flight.FareClasses.Any())
        {
            return new ServicesResponse(false, "Add at least one fare class before publishing");
        }

        flight.Status = FlightStatus.OnSale;
        var result = await flightInterface.UpdateAsync(flight);

        return result > 0 ? new ServicesResponse(true, "Flight published successfully")
            : new ServicesResponse(false, "Failed to publish flight");
    }

    public async Task<IEnumerable<FlightListItemResponse>> GetAllAsync(string? origin, string? destination, DateTime? date)
    {
        var query = context.Flights
            .Where(f => f.Status == FlightStatus.OnSale)
            .Include(f => f.FareClasses)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(origin))
            query = query.Where(f => f.Origin == origin);

        if (!string.IsNullOrWhiteSpace(destination))
            query = query.Where(f => f.Destination == destination);

        if (date.HasValue)
            query = query.Where(f => f.DepartureAt.Date == date.Value.Date);

        var flights = await query.ToListAsync();

        return flights.Select(f => f.FlightToListItemMapper());
    }

    public async Task<FlightResponse> GetByIdAsync(int flightId)
    {
        // Fix #1 تاني هنا
        var flight = await flightInterface.GetByIdAsync(flightId, f => f.FareClasses);

        if (flight is null)
        {
            throw new ItemNotFoundException("Flight not found");
        }

        return flight.FlightToResponseMapper();
    }


}
