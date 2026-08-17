public class QueueStatusResponse
{
    public int QueuePosition { get; set; }
    public QueueStatus Status { get; set; }
    public int EstimatedWaitSeconds { get; set; }
    public DateTime? ExpiresAt { get; set; }
}