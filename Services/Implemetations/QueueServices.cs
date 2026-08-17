using Microsoft.EntityFrameworkCore;

public class QueueServices(ApplicationDbContext context) : IQueueServices
{
    public async Task<QueueStatusResponse> JoinQueueAsync(int flightId, Guid userId)
    {
        var flight = await context.Flights.FindAsync(flightId);

        if (flight == null || flight.Status != FlightStatus.OnSale)
        {
            throw new ItemNotFoundException("Flight is not available for booking");
        }

        if (DateTime.UtcNow < flight.SalesStartAt)
        {
            throw new InvalidOperationException("Booking has not opened yet for this flight");
        }

        var existingTicket = await context.QueueTickets
            .FirstOrDefaultAsync(q => q.FlightId == flightId && q.UserId == userId
                && q.Status != QueueStatus.Expired);

        if (existingTicket != null)
        {
            return existingTicket.QueueTicketToStatusMapper();
        }

        // Fix #7: كان بيعد كل الـ tickets (حتى المنتهية) → الترتيب كان بيكبر غلط بمرور الوقت
        int lastPosition = await context.QueueTickets
            .Where(q => q.FlightId == flightId
                && (q.Status == QueueStatus.Waiting || q.Status == QueueStatus.Active))
            .CountAsync();

        var queueTicket = new QueueTicket
        {
            UserId = userId,
            FlightId = flightId,
            QueuePosition = lastPosition + 1,
            Status = QueueStatus.Waiting,
        };

        context.QueueTickets.Add(queueTicket);
        await context.SaveChangesAsync();

        return queueTicket.QueueTicketToStatusMapper();
    }

    public async Task<QueueStatusResponse> GetStatusAsync(Guid queueTicketId, Guid userId)
    {
        var ticket = await context.QueueTickets.FindAsync(queueTicketId);

        if (ticket == null)
        {
            throw new ItemNotFoundException("Queue ticket not found");
        }

        if (ticket.UserId != userId)
        {
            throw new UnauthorizedAccessException("This queue ticket does not belong to you");
        }

        if (ticket.Status == QueueStatus.Active && ticket.ExpiresAt < DateTime.UtcNow)
        {
            ticket.Status = QueueStatus.Expired;
            await context.SaveChangesAsync();
        }

        return ticket.QueueTicketToStatusMapper();
    }
}
