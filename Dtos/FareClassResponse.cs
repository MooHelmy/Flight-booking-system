public class FareClassResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int AvailableSeats { get; set; }
}
// FareClassResponse  // ده وظيفته تسمح لـ Client بإرجاع FareClass من غير الـ FlightId