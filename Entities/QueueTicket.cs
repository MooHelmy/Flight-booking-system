public class QueueTicket
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int FlightId { get; set; }
    public Flight? Flight { get; set; }

    public Guid UserId { get; set; }
    public int QueuePosition { get; set; }
    public QueueStatus Status { get; set; }

    // Nullable لأنها بتتحدد بس لما الـ Background Job يخلي الـ ticket Active
    public DateTime? ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
