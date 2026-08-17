public class CreateFareClassRequest
{
    public int FlightId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TotalSeats { get; set; }
    public int OverbookingLimit { get; set; }
}