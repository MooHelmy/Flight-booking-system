public class CreateFareClassRequest
{
    public Guid FlightId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int TotalSeats { get; set; }
    public int OverbookingLimit { get; set; }
}
//CreateFareClassRequest  // ده وظيفته تسمح لـ Client بإنشاء FareClass جديد من غير الـ FlightId
// بارة من الـ DTO اللي يتم إرساله عن طريق POST لـ FareClass
