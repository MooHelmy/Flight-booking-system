public class FareClassServices(ApplicationDbContext context) : IFareClassServices
{
    public async Task<ServicesResponse<int>> CreateAsync(CreateFareClassRequest dto, Guid airlineStaffId)
    {
        var flight = await context.Flights.FindAsync(dto.FlightId);

        if (flight == null)
        {
            return new ServicesResponse<int>(false, "Flight not found");
        }

        if (flight.AirlineStaffId != airlineStaffId)
        {
            return new ServicesResponse<int>(false, "You are not allowed to modify this flight");
        }

        if (dto.TotalSeats <= 0)
        {
            return new ServicesResponse<int>(false, "Total seats must be greater than 0");
        }

        // Fix #8: OverbookingLimit مينفعش يكون سالب
        if (dto.OverbookingLimit < 0)
        {
            return new ServicesResponse<int>(false, "Overbooking limit cannot be negative");
        }

        if (dto.OverbookingLimit > dto.TotalSeats * 0.10)
        {
            return new ServicesResponse<int>(false, "Overbooking limit cannot exceed 10% of total seats");
        }

        var fareClass = dto.FareClassToEntityMapper();

        context.FarClasses.Add(fareClass);
        var result = await context.SaveChangesAsync();

        return result > 0
            ? new ServicesResponse<int>(true, "Fare class created successfully", fareClass.Id)
            : new ServicesResponse<int>(false, "Fare class not created");
    }
}
