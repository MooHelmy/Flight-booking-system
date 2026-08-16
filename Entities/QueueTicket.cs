public class QueueTicket
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid FlightId { get; set; }
    public int QueuePosition { get; set; }
    public QueueStatus Status { get; set; }
    public DateTime? ActivatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
