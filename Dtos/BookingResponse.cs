// CompleteProfileRequest هي من الـ DTO اللي يتم إرساله عن طريق POST لـ Identity بالكامل
public class BookingResponse
{
    public Guid Id { get; set; }
    public string BookingReference { get; set; }
    public string FlightNumber { get; set; }    // من جدول Flight (Join)
    public string Origin { get; set; }          // من جدول Flight (Join)
    public string Destination { get; set; }      // من جدول Flight (Join)
    public DateTime DepartureAt { get; set; }    // من جدول Flight (Join)
    public string FareClassName { get; set; }    // من جدول FareClass (Join)
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime ConfirmedAt { get; set; }
}
