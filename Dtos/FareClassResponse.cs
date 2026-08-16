public class FareClassResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int AvailableSeats { get; set; }
    // Computed: TotalSeats + OverbookingLimit - BookedSeats
}
// FareClassResponse  // ده وظيفته تسمح لـ Client بإرجاع FareClass من غير الـ FlightId