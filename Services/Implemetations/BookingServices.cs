using Microsoft.EntityFrameworkCore;

public class BookingServices(ApplicationDbContext context) : IBookingServices
{
    public async Task<IEnumerable<BookingResponse>> GetAllAsync(Guid userId)
    {
        var bookings = await context.Bookings
            .Include(b => b.SeatHold).ThenInclude(h => h.FareClass).ThenInclude(fc => fc.Flight)
            .Where(b => b.UserId == userId && b.Status == BookingStatus.Confirmed)
            .OrderByDescending(b => b.ConfirmedAt)
            .ToListAsync();

        return bookings.Select(b => b.BookingToResponseMapper());
    }

    public async Task<BookingResponse> GetByIdAsync(Guid bookingId, Guid userId)
    {
        var booking = await context.Bookings
            .Include(b => b.SeatHold).ThenInclude(h => h.FareClass).ThenInclude(fc => fc.Flight)
            .FirstOrDefaultAsync(b => b.Id == bookingId);

        if (booking == null)
        {
            throw new ItemNotFoundException("Booking not found");
        }

        if (booking.UserId != userId)
        {
            throw new UnauthorizedAccessException("This booking does not belong to you");
        }

        return booking.BookingToResponseMapper();
    }
}
