using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("[controller]")]
[Authorize]
public class BookingController(IBookingServices bookingServices) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllAsync(Guid userId)
    {
        var bookings = await bookingServices.GetAllAsync(userId);
        return bookings.Any() ? Ok(bookings) : NotFound(bookings);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(Guid id, Guid userId)
    {
        var booking = await bookingServices.GetByIdAsync(id, userId);
        return booking is null ? NotFound(booking) : Ok(booking);
    }
}