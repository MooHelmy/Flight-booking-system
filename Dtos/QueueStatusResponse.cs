// CompleteProfileRequest هي من الـ DTO اللي يتم إرساله عن طريق POST لـ Identity بالكامل
public class QueueStatusResponse
{
    public int QueuePosition { get; set; }
    public QueueStatus Status { get; set; }
    public int EstimatedWaitSeconds { get; set; } // Computed
    public DateTime? ExpiresAt { get; set; }
}
