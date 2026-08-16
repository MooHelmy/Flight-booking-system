// CompleteProfileRequest هي من الـ DTO اللي يتم إرساله عن طريق POST لـ Identity بالكامل
public class HoldResponse
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }     // Computed: Quantity * FareClass.Price
    public int SecondsRemaining { get; set; }   // Computed من ExpiresAt
    public HoldStatus Status { get; set; }
}
