public interface IBookingServices
{
    Task<IEnumerable<BookingResponse>> GetAllAsync(Guid userId);
    Task<BookingResponse> GetByIdAsync(Guid bookingId, Guid userId);
}