// CompleteProfileRequest هي من الـ DTO اللي يتم إرساله عن طريق POST لـ Identity بالكامل
public class CreateHoldRequest
{
    public Guid FareClassId { get; set; }
    public int Quantity { get; set; }
}
