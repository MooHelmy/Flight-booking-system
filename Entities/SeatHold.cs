// CompleteProfileRequest هي من الـ DTO اللي يتم إرساله عن طريق POST لـ Identity بالكامل
public class SeatHold
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid FareClassId { get; set; }
    public int Quantity { get; set; }
    public HoldStatus Status { get; set; }
    public DateTime HeldAt { get; set; }
    public DateTime ExpiresAt { get; set; }
}
